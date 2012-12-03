function imname = bodir(datadir)
% written by Liefeng Bo on 01/04/2011 in University of Washington

% remove rootdir
imname = dir(datadir);
imname(1:2) = [];

