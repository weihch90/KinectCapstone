function feature = extract_feature_normal(fea_first, dic_first)
% Batch orthogonal matching pursuit and spatial pyramid max pooling in the
% first layer;
% Generate image-level features via spatial pyramid max pooling and
% contrast normalization.
% Original written by Liefeng Bo on August 2012; Modified by Jiaqi Wang on
% December 2012.

addpath(genpath('../helpfun'));
addpath(genpath('../ksvdbox/'));
dic_first.G = dic_first.dic'*dic_first.dic;

% read raw data
im = fea_first.pixels;
im = double(im);
topleft = [0 0];
pcloud = depthtocloud(im, topleft);
pcloud = pcloud./1000; % normalized to meter
normal = pcnormal(pcloud);
im = normal;

% resize images
im_h = size(im,1);
im_w = size(im,2);
im = imresize(im, fea_first.maxsize/max(im_h, im_w), 'bicubic');

encoder_first.coding = 'omp';
encoder_first.pooling = 4;
encoder_first.sparsity = 4;
   
% batch Orthogonal Matching Pursuit
if size(im,3) == 1
   X = im2colstep(im, [dic_first.patchsize dic_first.patchsize], [1 1] );
else
   X = im2colstep(im, [dic_first.patchsize dic_first.patchsize size(im,3)], [1 1 1] );
end

X = remove_dc(X, 'columns');

DxX = (sqrt(size(dic_first.dic,1))*dic_first.dic')*X;
omp_codes.codes = omp(DxX, dic_first.G, encoder_first.sparsity);

omp_codes.codes = abs(omp_codes.codes); 
omp_codes.height = size(im,1)-dic_first.patchsize+1;
omp_codes.width = size(im,2)-dic_first.patchsize+1;
        
% max pooling
py = floor(omp_codes.height/encoder_first.pooling);
px = floor(omp_codes.width/encoder_first.pooling);
ind_b = [];
for i = 1:encoder_first.pooling
    ind_b = [ind_b; (1:encoder_first.pooling)'+(i-1)*omp_codes.height];
end

pooling = sparse(zeros(size(omp_codes.codes,1),px*py));

for i = 1:px
    for j = 1:py
        ind_s = encoder_first.pooling*(j-1)+(i-1)*encoder_first.pooling*omp_codes.height;
        ind_p = ind_b + ind_s;
        pooling(:,(i-1)*py+j) = max(omp_codes.codes(:,ind_p(:)),[],2);
        % omp_pooling((i-1)*py+j,:) = sqrt(mean(abs(codes).^2, 2));
    end
end

omp_pooling.pooling = pooling;
omp_pooling.height = py;
omp_pooling.width = px;

%
% Omp Pooling Final
%

% initialize the parameters of encoder
encoder_final.coding = 'omp';
encoder_final.pooling = [1 2 3];
encoder_final.patchsize = 1;

% set the parameters

if size(encoder_final.pooling,1) == 1
   encoder_final.pooling = [encoder_final.pooling; encoder_final.pooling];
end

omp_pooling.pooling = full(omp_pooling.pooling)';
omp_pooling.pooling = reshape(omp_pooling.pooling,omp_pooling.height,omp_pooling.width,size(omp_pooling.pooling,2));
omp_fea = omp_patchfea(omp_pooling.pooling, encoder_final.patchsize);

imfea_one = omp_pooling_final(omp_fea, encoder_final);
feature = single(imfea_one);

