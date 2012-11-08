
% written by Liefeng Bo on 01/04/2011 in University of Washington

clear;

% add paths
addpath('liblinear-1.5-dense-float/matlab');
addpath('helpfun');
addpath('omp_layer1');
addpath('omp_layer2');
addpath('learn_layer1');
addpath('learn_layer2');
addpath(genpath('ksvdbox/'));

% compute the paths of images
imdir = 'sampleImages/';
imsubdir = dir_bo(imdir);
impath = [];
rgbdclabel = [];
rgbdilabel = [];
rgbdvlabel = [];
subsample = 1;
label_num = 0;
for i = 1:length(imsubdir)
    [rgbdilabel_tmp, impath_tmp] = get_im_label([imdir imsubdir(i).name '/'], '_depthcrop.png');
    for j = 1:length(impath_tmp)
        ind = find(impath_tmp{j} == '_');
        rgbdvlabel_tmp(1,j) = str2num(impath_tmp{j}(ind(end-2)+1));
    end

    it = 0;
    for j = 1:subsample:length(impath_tmp)
        it = it + 1;
        impath_tmp_sub{it} = impath_tmp{j};
    end
    impath = [impath impath_tmp_sub];
    rgbdclabel = [rgbdclabel i*ones(1,length(impath_tmp_sub))];
    rgbdilabel = [rgbdilabel rgbdilabel_tmp(1:subsample:end)+label_num];
    rgbdvlabel = [rgbdvlabel rgbdvlabel_tmp(1:subsample:end)];
    label_num = label_num + length(unique(rgbdilabel_tmp));
    clear impath_tmp_sub rgbdvlabel_tmp;
end

% first layer of HMP
% initialize the parameters of dictionary
dic_first.dicsize = 500;
dic_first.patchsize = 16;
dic_first.samplenum = 100;

% initialize the paramters of feature (data)
fea_first.feapath = impath;
fea_first.type = 'depth';
fea_first.maxsize = 150;
fea_first.savedir = ['./features/rgbdhomp_ksvd_first_' num2str(dic_first.patchsize) 'x' num2str(dic_first.patchsize) '_' fea_first.type '/'];

bomkdir(fea_first.savedir);
rgbdfeapath = get_fea_path(fea_first.savedir);
if ~length(rgbdfeapath)
   % dictionary learning
   dic = ksvd_learn_layer1(fea_first, dic_first);
   save(['./rgbd_dic_' num2str(dic_first.patchsize) 'x' num2str(dic_first.patchsize) '_' fea_first.type '.mat'],'dic');
   dic_first.dic = dic;
   % initialize the parameters of encoder
   encoder_first.coding = 'omp';
   encoder_first.pooling = 4;
   encoder_first.sparsity = 4;
   % orthogonal matching pursuit encoder
   omp_pooling_layer1_batch(fea_first, dic_first, encoder_first);
   rgbdfeapath = get_fea_path(fea_first.savedir);
end

% feature layer of HMP
% initialize the paramters of features
fea_final.feapath = rgbdfeapath;
% initialize the parameters of encoder
encoder_final.coding = 'omp';
encoder_final.pooling = [1 2 3];
encoder_final.patchsize = 1;
rgbdfea = omp_pooling_final_batch_single(fea_final,encoder_final);

save -v7.3 rgbdfea_depth_first rgbdfea rgbdclabel rgbdilabel rgbdvlabel;

load testinstance1;
category = 1;
if category
   trail = 10;
   for i = 1:trail
       % generate training and test samples
       ttrainindex = [];
       ttestindex = [];
       labelnum = unique(rgbdclabel);
       for j = 1:length(labelnum)
           trainindex = find(rgbdclabel == labelnum(j));
           rgbdilabel_unique = unique(rgbdilabel(trainindex));
           %perm = randperm(length(rgbdilabel_unique));
           subindex = find(rgbdilabel(trainindex) == rgbdilabel_unique(testinstance(i,j)));
           testindex = trainindex(subindex);
           trainindex(subindex) = [];
           ttrainindex = [ttrainindex trainindex];
           ttestindex = [ttestindex testindex];
       end

       % train linear SVM
       trainfea = rgbdfea(:,ttrainindex)';
       trainlabel = rgbdclabel(ttrainindex)'; % take category label
       % classify with liblinear
       lc = 10;
       option = ['-s 1 -c ' num2str(lc)];
       model = train(trainlabel,trainfea,option,'','trainfea');
       % compute classification accuracy
       testfea = rgbdfea(:,ttestindex)';
       testlabel = rgbdclabel(ttestindex)'; % take category label
       [predictlabel, accuracy, decvalues] = predict(testlabel, testfea, model);
       acc_c(i,1) = mean(predictlabel == testlabel);
       save(['./results/depth_acc_c_' num2str(dic_first.patchsize) 'x' num2str(dic_first.patchsize) '_first.mat'],'acc_c', 'predictlabel', 'testlabel');

       % print and save results
       disp(['Accuracy of Liblinear is ' num2str(mean(acc_c))]);
   end
end

instance = 1;
if instance

   % generate training and test indexes
   indextrain = 1:length(rgbdilabel);
   indextest = find(rgbdvlabel == 2);
   indextrain(indextest) = [];

   % generate training and test samples
   trainfea = rgbdfea(:, indextrain)';
   trainlabel = rgbdilabel(:, indextrain)';
   disp('Performing liblinear ... ...');
   lc = 10;
   % classify with liblinear
   option = ['-s 1 -c ' num2str(lc)];
   model = train(trainlabel,trainfea,option,'', 'trainfea');

   testfea = rgbdfea(:, indextest)';
   testlabel = rgbdilabel(:, indextest)';
   [predictlabel, accuracy, decvalues] = predict(testlabel, testfea, model);
   acc_i = mean(predictlabel+1 == testlabel);
   save(['./results/depth_acc_i_' num2str(dic_first.patchsize) 'x' num2str(dic_first.patchsize) '_first.mat'],'acc_i', 'predictlabel', 'testlabel');

   % print and save classification accuracy
   disp(['Accuracy of Liblinear is ' num2str(mean(acc_i))]);
end