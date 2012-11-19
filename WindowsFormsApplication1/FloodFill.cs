using Microsoft.Kinect;
using System;
using System.Collections.Generic;

namespace GestureStudio
{   
    public class FloodFill
    {
        private const int MAX_SIZE = 10000;
        private const int SPREAD_METHOD = 0;
        private const int CLOSEST_DEPTH_DISTANCE = 80;
        private const int MAX_DISTANCE = 2047;
        private const int HAND_DEPTH = 200;
        private const int MIN_DEPTH = 200;
        
        private bool activateSecondHand;
        private short closestDistance;
        private short secondClosestDistance;
        private int cropStartX;        
        private int cropStartY;

        public FloodFill()
        {
            this.closestDistance = MAX_DISTANCE;
            this.secondClosestDistance = MAX_DISTANCE;
            this.cropStartX = 0;
            this.cropStartY = 0;
            activateSecondHand = false;
        }

        public bool ActivateSecondHand
        {
            get
            {
                return activateSecondHand;
            }

            set
            {
                activateSecondHand = value;
            }
        }

        public short SecondClosestDistance
        {
            get { return secondClosestDistance; }
        }

        public short ClosestDistance
        {
            get { return closestDistance; }
        }

        public int CropStartX
        {
            get { return cropStartX; }
        }

        public int CropStartY
        {
            get { return cropStartY; }
        }

        public DepthFrame Process(short[] depthPixels, int height, int width)
        {
            short[] secondDepthPixels = null;
            if (activateSecondHand) {
                secondDepthPixels = (short[])depthPixels.Clone();
            }
            // initialize flood fill data
            FloodFillImageData floodFillData = new FloodFillImageData();
            int closestIndex = this.FindClosestPixel(depthPixels, new HashSet<int>(floodFillData.l), out this.closestDistance);

            int[] rect = new int[4];
            rect[0] = rect[2] = closestIndex % width;
            rect[1] = rect[3] = closestIndex / width;
            floodFillData.rect = rect;

            //Perform flood fill on the current frame and find the cropping area.
            DoFill(depthPixels, width, height, closestIndex, floodFillData);

            if (activateSecondHand)
            {
                int secondClosestIndex = this.FindClosestPixel(secondDepthPixels, new HashSet<int>(floodFillData.l), out this.secondClosestDistance);
                if (Math.Abs(this.secondClosestDistance - this.closestDistance) < CLOSEST_DEPTH_DISTANCE)
                    DoFill(secondDepthPixels, width, height, secondClosestIndex, floodFillData);
            }
            this.cropStartX = floodFillData.rect[0];
            this.cropStartY = floodFillData.rect[1];
            return cropImage(floodFillData, width, depthPixels);
        }

        private DepthFrame cropImage(FloodFillImageData floodFillData, int width, short[] rawDepth)
        {
            int startX = floodFillData.rect[0];
            int startY = floodFillData.rect[1];
            int endX = floodFillData.rect[2];
            int endY = floodFillData.rect[3];

            int croppedWidth = (endX - startX + 1);
            int croppedHeight = (endY - startY + 1);

            short[] filledCrop = new short[croppedHeight * croppedWidth];
            foreach (int i in floodFillData.l)
            {
                int x = i % width;
                int y = i / width;
                int offset = startY * width + startX;
                int transposedX = x - startX;
                int transposedY = y - startY;
                int transposedIndex = transposedX + transposedY * croppedWidth;
                filledCrop[transposedIndex] = rawDepth[i];
            }

            return new DepthFrame() { Pixels = filledCrop, Width = croppedWidth, Height = croppedHeight };
        }

        private int FindClosestPixel(short[] depthPixels, HashSet<int> ignorePixels, out short closestDistance)
        {
            int closestIndex = 0;
            closestDistance = MAX_DISTANCE;
            for (int i = 0; i < depthPixels.Length; ++i)
            {
                // discard the portion of the depth that contains only the player index
                short depth = (short)(depthPixels[i] >> DepthImageFrame.PlayerIndexBitmaskWidth);
                depthPixels[i] = depth;

                if (!ignorePixels.Contains(i) && depth > MIN_DEPTH && depth < closestDistance)
                {
                    closestDistance = depth;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }
        
        private void Spread(int index, int origin, LinkedList<int> linked, HashSet<int> discovered, short[] rawDepth)
        {
            if (!discovered.Contains(index) && 0 <= index && index < rawDepth.Length)
            {
                if (linked.First == null) {
                    linked.AddFirst(index);
                    return;
                }
                for (LinkedListNode<int> it = linked.First; it != null; it = it.Next)
                {
                    if (Math.Abs((ushort)rawDepth[it.Value] - (ushort)rawDepth[origin])
                     > Math.Abs((ushort)rawDepth[index] - (ushort)rawDepth[origin]))
                    {
                        linked.AddBefore(it, index);
                        return;
                    }
                    if (it.Next == null)
                    {
                        linked.AddLast(index);
                        return;
                    }
                }
            }
        }


        // Perform flood fill algorithm on the current frame
        // width and height correspond to those of the current frame.
        private void DoFill(short[] rawDepth, int width, int height, int closestIndex, FloodFillImageData floodFillData)
        {
            Queue<int> q = new Queue<int>();
            List<int> l = floodFillData.l;
            HashSet<int> discovered = new HashSet<int>(floodFillData.l);
            int[] rect = floodFillData.rect;
            int count = 0;
            q.Enqueue(closestIndex);

            while (q.Count != 0 && count < MAX_SIZE)
            {
                int n = q.Dequeue();

                int x = n % width;
                int y = n / width;
                // check if the index is already discovered
                if (!discovered.Contains(n))
                {
                    // check if the index is within the range
                    if (0 <= x && x < width && 0 <= y && y < height)
                    {
                        // check if the index is within the distance
                        if ((ushort)rawDepth[n] - (ushort)rawDepth[closestIndex] <= HAND_DEPTH)
                        {
                            rect[0] = Math.Min(x, rect[0]);
                            rect[1] = Math.Min(y, rect[1]);

                            rect[2] = Math.Max(x, rect[2]);
                            rect[3] = Math.Max(y, rect[3]);
                            l.Add(n);
                            count++;
                            if (SPREAD_METHOD == 0)
                            {
                                // add node in each direction
                                if (!discovered.Contains(y * width + x + 1))
                                    q.Enqueue(y * width + x + 1);    // east

                                if (!discovered.Contains(y * width + x - 1))
                                    q.Enqueue(y * width + x - 1);    // west

                                if (!discovered.Contains((y + 1) * width + x))
                                    q.Enqueue((y + 1) * width + x);  // south

                                if (!discovered.Contains((y - 1) * width + x))
                                    q.Enqueue((y - 1) * width + x);  // north

                            }
                            else if (SPREAD_METHOD == 1)
                            {
                                LinkedList<int> linked = new LinkedList<int>();
                                Spread(y * width + x + 1, n, linked, discovered, rawDepth);
                                Spread(y * width + x - 1, n, linked, discovered, rawDepth);
                                Spread((y + 1) * width + x, n, linked, discovered, rawDepth);
                                Spread((y - 1) * width + x, n, linked, discovered, rawDepth);
                                for (LinkedListNode<int> it = linked.First; it != null; it = it.Next)
                                {
                                    q.Enqueue(it.Value);
                                }
                            }
                        }
                    }
                }

                discovered.Add(n);
                
            }

            floodFillData.rect = rect;
        }

        class FloodFillImageData
        {
            public List<int> l;
            public HashSet<int> discovered;
            public int[] rect;

            public FloodFillImageData()
            {
                this.l = new List<int>();
                this.discovered = new HashSet<int>();
            }
            public FloodFillImageData(List<int> l, HashSet<int> discovered, int[] rect)
            {
                this.l = l;
                this.discovered = discovered;
                this.rect = rect;
            }
        }
    }
}
