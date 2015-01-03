function X=getBezierPoints(knots,cp1,cp2,N)

n=size(cp1,1);
dim=size(cp1,2);

t=[0:1/N:1-1/N]';
t=repmat(t,1,dim);
lt=size(t,1);

cpnts=cat(3, knots(1:end-1,:), cp1, cp2, knots(2:end,:));
X=[];
for i=1:n
    B=repmat(cpnts(i,:,1),lt,1).*((1-t).^3) + 3*repmat(cpnts(i,:,2),lt,1).*(t.*(1-t).^2) + 3*repmat(cpnts(i,:,3),lt,1).*((t.^2).*(1-t))+repmat(cpnts(i,:,4),lt,1).*(t.^3);
    X=[X ;B];
end
X=[X ;X(1,:)];
