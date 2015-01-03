function N=getNormal(X)
%Normals for a closed curve
%curve is assumed to be going around in a clockwise direction
%so normals will stick out on the left of curve
Y=[X(:,length(X)) X X(:,1)];
N=[0 -1; 1 0]*(Y(:,3:end)-Y(:,1:end-2));
N=N./repmat(sqrt(sum(N.*N)),2,1);