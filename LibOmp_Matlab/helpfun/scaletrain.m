function [fea, minvalue, maxvalue] = scaletrain(fea, type)
% normalize features. This step empirically improves the performance

minvalue = min(fea,[],2);
maxvalue = max(fea,[],2);
switch type
  case 'no'
        fea = fea;
  case 'linear'
	for i = 1:size(fea,2)
	    fea(:,i) = (fea(:,i) - minvalue)./(maxvalue - minvalue);
            if mod(i,100) == 1
               disp(['Current Iteration is ' num2str(i)]);
            end
	end
  case 'standard'
        minvalue = mean(fea,2);
        maxvalue = sqrt(var(fea,0,2)+0.001);
        for i = 1:size(fea,2)
            fea(:,i) = (fea(:,i) - minvalue)./maxvalue;
        end
  case 'hiscode'
        % histogram intersection code
        U = HISCode;
        tfea = sparse(size(U,1)*size(fea,1),size(fea,2));
        for i = 1:size(fea,2)
            hiscode = U(:,(fea(:,i) + 1));
            tfea(:,i) = hiscode(:);
        end
        fea = tfea;
  case 'qhiscode'
        % histogram intersection code
        U = QHISCode;
        tfea = sparse(size(U,1)*size(fea,1),size(fea,2));
        for i = 1:size(fea,2)
            hiscode = U(:,floor((fea(:,i) + 1)/2)+1);
            tfea(:,i) = hiscode(:);
        end
        fea = tfea;
  case 'power'
        ppp = 0.5;
        for i = 1:size(fea,2)
            fea(:,i) = sign(fea(:,i)).*abs(fea(:,i)).^ppp;
        end
  case 'dpower'
        pl = 0.3;
        pu = 0.7;
        d = size(fea,1);
        fea = [fea; fea];
        for i = 1:size(fea,2)
            fea(:,i) = [sign(fea(1:d,i)).*abs(fea(1:d,i)).^pl; sign(fea(d+1:end,i)).*abs(fea(d+1:end,i)).^pu];
        end
  case 'sigmoid'
        gamma = 100;
        for i = 1:size(fea,2)
            % aaa = exp(gamma*fea(:,i));
            % bbb = 1./aaa;
            % fea(:,i) = (aaa-bbb)./(aaa+bbb);
            aaa = exp(-gamma*fea(:,i));
            fea(:,i) = 1./(1 + aaa);
        end
  case 'his'
        for i = 1:size(fea,2)
            fea(:,i) = (fea(:,i) - minvalue);
        end
  case 'lcode'
       load code;
       [feaindex, feascore] = getvindex(fea, 'emk', grid);
       fea = getvcode(feaindex, feascore, code, 'emk', grid);
  otherwise
       disp('Unknown type');
end

