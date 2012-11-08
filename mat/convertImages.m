imageFiles = dirrec('sampleImages', '.png');

for n=1:length(imageFiles)
   filePath = char(imageFiles(n));   
   image = imread(filePath);
   image = colorToDepth(image);
   filePath = strrep(filePath, '.png', '_depthcrop.png');
   imwrite(image, filePath);
   
   if mod(n, 50) == 0 
       disp(['finished ' int2str(n) ' out of ' int2str(length(imageFiles)) ' files']);
   end 
end