using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GestureStudio
{
    class Png16Reader
    {
        public Png16Reader()
        {
        }

        public short[,] Read(string filePath)
        {
            short[,] imagePixels = null;

            FileStream fs = File.Open(filePath, FileMode.Open);
            using (BinaryReader reader = new BinaryReader(fs))
            {
                if (ReadHeader(reader))
                {
                    int width, height;
                    byte depth;

                    if (ReadIHDR(reader, out width, out height, out depth))
                    {
                        string chunkType = "";

                        while (chunkType != "IEND")
                        {
                            byte[] chunkData = ReadChunk(reader, out chunkType);

                            // we only support image with one IDAT
                            if (chunkType == "IDAT")
                            {
                                imagePixels = DecompressImageData(chunkData, width, height);
                            }
                        }
                    }
                }
            }

            return imagePixels;
        }


        bool ReadHeader(BinaryReader reader)
        {
            byte[] pngHeader = {
                                   0x89,
                                   0x50, 0x4E, 0x47,
                                   0x0D, 0x0A,
                                   0x1A,
                                   0x0A
                               };
            for (int i = 0; i < pngHeader.Length; ++i)
            {
                byte value = reader.ReadByte();
                if (value != pngHeader[i])
                {
                    return false;
                }
            }

            return true;
        }

        bool ReadIHDR(BinaryReader reader, out int width, out int height, out byte depth)
        {
            byte[] ihdr = ReadChunk(reader);
            if (ihdr != null)
            {
                width = byteToInt32(ihdr, 0);
                height = byteToInt32(ihdr, 4);
                depth = ihdr[8];

                return true;
            }

            width = 0;
            height = 0;
            depth = 0;
            return false;
        }

        byte[] ReadChunk(BinaryReader reader)
        {
            string chunkType;
            return ReadChunk(reader, out chunkType);
        }

        byte[] ReadChunk(BinaryReader reader, out string chunkType)
        {
            byte[] length = reader.ReadBytes(4);
            byte[] type = reader.ReadBytes(4);

            chunkType = System.Text.Encoding.ASCII.GetString(type);

            int dataLength = byteToInt32(length, 0);

            byte[] buffer = null;
            if (dataLength > 0)
            {
                buffer = new byte[dataLength];
                reader.Read(buffer, 0, buffer.Length);
            }

            // crc
            reader.ReadBytes(4);

            return buffer;
        }

        int byteToInt32(byte[] data, int startIndex)
        {
            return data[0 + startIndex] << 24 |
                data[1 + startIndex] << 16 |
                data[2 + startIndex] << 8 |
                data[3 + startIndex];
        }

        short[,] DecompressImageData(byte[] data, int width, int height)
        {
            // Decompress with deflate, then remove filter

            short[,] pixels = new short[height, width];
            using (MemoryStream imageData = new MemoryStream())
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    // skip 2 bytes for zlib specs
                    ms.ReadByte();
                    ms.ReadByte();
                    using (System.IO.Compression.DeflateStream deflate = new System.IO.Compression.DeflateStream(ms, System.IO.Compression.CompressionMode.Decompress, false))
                    {
                        deflate.CopyTo(imageData);
                    }
                }
                
                // Remove Filter
                imageData.Seek(0, SeekOrigin.Begin);

                for (int i = 0; i < height; ++i)
                {
                    // beginning of scanline, there's one byte of filter type
                    byte filterType = (byte)imageData.ReadByte();
                    for (int j = 0; j < width; ++j)
                    {
                        // within the scanline, each pixel is 2 bytes
                        byte hiX = (byte)imageData.ReadByte();
                        byte loX = (byte)imageData.ReadByte();

                        byte hiA = 0;
                        byte loA = 0;
                        byte hiB = 0;
                        byte loB = 0;
                        byte hiC = 0;
                        byte loC = 0;

                        if (j > 0)
                        {
                            // A = previous pixel
                            hiA = (byte)(pixels[i, j - 1] >> 8);
                            loA = (byte)(pixels[i, j - 1] & 0xFF);
                        }

                        // B = pixel above scanline
                        if (i > 0)
                        {
                            hiB = (byte)(pixels[i - 1, j] >> 8);
                            loB = (byte)(pixels[i - 1, j] & 0xFF);
                        }

                        if (i > 0 && j > 0)
                        {
                            // C previous pixel above scanline
                            hiC = (byte)(pixels[i - 1, j - 1] >> 8);
                            loC = (byte)(pixels[i - 1, j - 1] & 0xFF);
                        }

                        pixels[i, j] = (short)(ReconstructFilteredByte(filterType, hiX, hiA, hiB, hiC) << 8 |
                            ReconstructFilteredByte(filterType, loX, loA, loB, loC));
                    }
                }
            }

            return pixels;
        }

        byte ReconstructFilteredByte(int filterType, byte x, byte a, byte b, byte c)
        {
            // data relationship:
            // c b
            // a x
            switch (filterType)
            {
                case 0:
                    return x;
                case 1:
                    return (byte)(x + a);
                case 2:
                    return (byte)(x + b);
                case 3:
                    return (byte)(x + (a + b) / 2);
                case 4:
                    return (byte)(x + PaethPredictor(a, b, c));
            }

            return x;
        }

        byte PaethPredictor(byte a, byte b, byte c)
        {
            int p = a + b - c;
            int pa = Math.Abs(p - a);
            int pb = Math.Abs(p - b);
            int pc = Math.Abs(p - c);

            if (pa <= pb && pa <= pc)
            {
                return a;
            }
            else if (pb < pc)
            {
                return b;
            }
            else
            {
                return c;
            }
        }
    }
}
