function kappa=getHerronCurvature(X,N)
%This is for a closed curve
n=length(X);
kappa=zeros(1,n);
for i=1:n
    if i==1
        A=(X(:,2)-X(:,1));
        B=(X(:,n)-X(:,2));
        C=(X(:,1)-X(:,n));
    elseif i==n
         A=(X(:,1)-X(:,n));
        B=(X(:,n-1)-X(:,1));
        C=(X(:,n)-X(:,n-1));
    else
       A=(X(:,i+1)-X(:,i));
        B=(X(:,i-1)-X(:,i+1));
        C=(X(:,i)-X(:,i-1));
    end
    a=sqrt(dot(A,A));
    b=sqrt(dot(B,B));
    c=sqrt(dot(C,C));
    
    s=(a+b+c)/2;
    r=(4*sqrt(s*(s-a)*(s-b)*(s-c)))/(a*b*c);
    kappa(i)=r*sign(dot(A,N(:,i)));
end

function dist=dist(x1,x2)

dist= sqrt(dot(x1,x1)+dot(x2,x2));