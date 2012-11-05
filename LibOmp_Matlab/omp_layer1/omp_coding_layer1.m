function omp_codes = omp_coding_layer1(im, dic_first, encoder_first)
% compute sparse codes by batch orthogonal matching pursuit in the first layer
% written by Liefeng Bo on August 2012

switch encoder_first.coding

    case 'omp'
        % overcomplete basis vectors
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

        %if strcmp(fea_first.type, 'rgbd') | strcmp(fea_first.type, 'grayd')
        %   X = remove_dc_multi(X, dic_first.group);
        %else
        %   X = remove_dc(X, 'columns');
        %end
        %codes = omp(DxX, G, encoder_first.sparsity, 'gammamode', 'full');
        %omp_codes = reshape(full(omp_codes'), size(im,1)-dic_first.patchsize+1,size(im,2)-dic_first.patchsize+1, dic_first.dicsize);
        % ompparam.L = param; % not more than 10 non-zeros coefficients
        % ompparam.eps = 0.0; % squared norm of the residual should be less than 0.1
        % ompparam.numThreads = -1; % number of processors/cores to use; the default choice is -1
                    % and uses all the cores of the machine
        % omp_codes = mexOMP(X, dic_first.dic, ompparam);
        % omp_codes = abs(reshape(full(omp_codes'), size(im,1)-dic_first.patchsize+1,size(im,2)-dic_first.patchsize+1, dic_first.dicsize));
        % omp_codes = double(omp_codes > 0); 

    case 'orthogonal'
        % orthogonal basis vectors
        for i = 1:size(dic_first.dic,2)
            code_one = 0;
            filter = reshape(dic_first.dic(:,i),dic_first.patchsize,dic_first.patchsize,size(im,3));
            for j = 1:size(im,3)
                code_one_tmp = filter2(filter(:,:,j), im(:,:,j), 'valid');
                code_one = code_one + code_one_tmp;
            end
            if i == 1
                omp_codes = zeros(size(code_one,1), size(code_one,2), size(dic_first.dic,2));
            end
            code_one = code_one(:);
            omp_codes(:,:,i) = abs(reshape(code_one,size(omp_codes,1),size(omp_codes,2)));
        end
        omp_codes(:,:,1) = [];

    case 'omp_one'
        % choose one basis vector from overcomplete basis vectors
        for i = 2:size(dic_first.dic,2)
            filter = reshape(dic_first.dic(:,i), dic_first.patchsize, dic_first.patchsize);
            dic_signal_one = filter2(filter, im, 'valid');
            if i == 2
                omp_codes = zeros(size(dic_signal_one,1), size(dic_signal_one,2), size(dic_first.dic,2)-1);
            end
            omp_codes(:,:,i-1) = dic_signal_one;
        end
        omp_codes = abs(omp_codes);
        max_codes = max(omp_codes, [], 3);
        for i = 1:size(omp_codes,3)
            omp_codes(:,:,i) = omp_codes(:,:,i).*(omp_codes(:,:,i) >= max_codes & omp_codes(:,:,i) >= 1e-6);
        end

   case 'localmean'
        wfilter = ones(dic_first.patchsize)/dic_first.patchsize^2;
        omp_codes.codes = 0;
        for i = 1:size(im,3)
            omp_codes.codes = omp_codes.codes + filter2(wfilter, im(:,:,i), 'valid');
        end
        omp_codes.codes = omp_codes.codes/size(im,3);
        omp_codes.height = size(omp_codes.codes,1);
        omp_codes.width = size(omp_codes.codes,2);
        omp_codes.codes = omp_codes.codes(:)';
    otherwise
        disp('unknown type');
end

