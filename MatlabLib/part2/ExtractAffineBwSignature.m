function [affine_signature] = ExtractAffineBwSignature(bw)


signutre = GetContourSortPerimeter(bw);

%This is our trivial initial parameterization
dt=1;

C=[signutre(:,4) signutre(:,3)];

%Define Derivative functions as centered.
Dt=@(r) (r(3:end,:)-r(1:end-2,:))/2/dt;
Dtt=@(r) (r(3:end,:)-2*r(2:end-1,:)+r(1:end-2,:))/dt^2;
Ct=Dt(C);
Ctt=Dtt(C);
k=(Ct(:,1).*Ctt(:,2)-Ct(:,2).*Ctt(:,1))./sqrt(Ct(:,1).^2+Ct(:,2).^2).^3;
theta=(atan(Ct(:,2)./Ct(:,1)));
kshear=k./(abs(cos(theta)).^3);
kshear((abs(kshear)>50))=50;%invariant curvature
dv=abs(Ct(:,1));
v=cumsum(dv);%invariant arclength
kshear=kshear/max(kshear);
affine_signature =[v kshear];

% max_s =max(signature(:,2));
% min_s=min(signature(:,2));
% signature(:,2)=(signature(:,2)-min_s)/(max_s);
% signature(:,2)=signature(:,2)-mean(signature(:,2));
% figure(18);plot(signature(:,1),signature(:,2));axis tight
