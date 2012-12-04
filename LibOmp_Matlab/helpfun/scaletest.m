function fea = scaletest(fea, type, minvalue, maxvalue)
% normalize features. This step empirically improves the performance

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
  case 'his'
        for i = 1:size(fea,2)
            fea(:,i) = max((fea(:,i) - minvalue), 0);
        end
  otherwise
       disp('Unknown type');
end

