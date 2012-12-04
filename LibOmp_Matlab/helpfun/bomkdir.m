function bomkdir(datadir)
% make a directory with checking whether there is an existing directory
% written by Liefeng Bo on 01/04/2011 in University of Washington

if exist(datadir,'dir')
   ;
else
   mkdir(datadir);
end

