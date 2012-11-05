% This make.m is used under Windows

mex -O -largeArrayDims -c ..\blas\*.c -outdir ..\blas
mex -O -largeArrayDims -c ..\linear.cpp -D_DENSE_REP
mex -O -largeArrayDims -c ..\tron.cpp -D_DENSE_REP
mex -O -largeArrayDims -c linear_model_matlab.c -D_DENSE_REP -I..\
mex -O -largeArrayDims train.c -D_DENSE_REP -I..\ tron.obj linear.obj linear_model_matlab.obj ..\blas\*.obj
mex -O -largeArrayDims predict.c -D_DENSE_REP -I..\ tron.obj linear.obj linear_model_matlab.obj ..\blas\*.obj
mex -O -largeArrayDims libsvmread.c
mex -O -largeArrayDims libsvmwrite.c
