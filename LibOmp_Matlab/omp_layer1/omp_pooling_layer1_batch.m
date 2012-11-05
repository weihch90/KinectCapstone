function omp_pooling_layer1_batch(fea_first, dic_first, encoder_first)
% batch orthogonal matching pursuit and spatial pyramid max pooling in the first layer
% written by Liefeng Bo on August 2012

if exist(fea_first.savedir,'dir')
    ;
else
    mkdir(fea_first.savedir);
end

disp('Batch orthogonal matching pursuit ...');
dic_first.G = dic_first.dic'*dic_first.dic;
for i = 1:length(fea_first.feapath)
    
    % read raw data
    switch fea_first.type
    case 'rgb'
       im = imread(fea_first.feapath{i});
       if size(im,3) == 1
          im = color(im);
       end
       im = im2double(im);
    case 'localmean'
       im = imread(fea_first.feapath{i});
       if size(im,3) == 1
          im = color(im);
       end
       im = im2double(im);
    case 'gray'
       im = imread(fea_first.feapath{i});
       if size(im,3) == 3
          im = rgb2gray(im);
       end
       im = im2double(im);
    case 'depth'
       im = imread(fea_first.feapath{i});
       im = imread(fea_first.feapath{i});
       threshold = 1200;
       im = double(im);
       im(im > threshold) = threshold;
       im = im/threshold;
    case 'normal'
       load(fea_first.feapath{i});
       im = normal;
    otherwise
       disp('unknown data type');
    end

    % resize images
    im_h = size(im,1);
    im_w = size(im,2);
    im = imresize(im, fea_first.maxsize/max(im_h, im_w), 'bicubic');
 
    % record feature extraction time
    tic;
    % batch Orthogonal Matching Pursuit
    omp_codes = omp_coding_layer1(im, dic_first, encoder_first);
    omp_pooling = omp_pooling_layer1(omp_codes, encoder_first);
    time = toc; 

    % save pooled features
    save([fea_first.savedir '/' sprintf('%06d',i)], 'omp_pooling');

    % print feature extraction information
    ind = find(fea_first.feapath{i} == '/');
    fprintf('Image ID %s: width= %d, height= %d, time %f\n', fea_first.feapath{i}(ind(end)+1:end), omp_pooling.width, omp_pooling.height, time);

end;

