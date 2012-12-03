function feapath = get_fea_dir(feadir)
% generate the image paths and the corresponding image labels
% written by Liefeng Bo on 01/04/2011 in University of Washington

% subdirectory
feaname = bodir(feadir);
if length(feaname)
   for i = 1:length(feaname)
       % generate image paths
       feapath{1,i} = [feadir '/' feaname(i).name];
   end
else
   feapath = [];
end

