function [r ang]=getMinShapeRadius(bw)
% Function returns the radius of the first zero pixel from the center of
% the image

% Assumes shape is centered at center of incircle of the shape
[num_rows,num_cols] = size(bw);
c=[num_cols num_rows]/2;

[X,Y]=meshgrid(1:num_cols,1:num_rows);
[theta,rho]=meshgrid(linspace(-pi,pi,num_cols),linspace(0,norm(c),num_rows));

I2=interp2(X,Y,double(bw),c(1)+cos(theta).*rho,c(2)+sin(theta).*rho,'nearest');
rIdx = find(any(I2==0,2),1,'first');
cIdx = find(I2(rIdx,:)==0,1,'first');
ang=theta(rIdx,cIdx)/pi*360;
r = rho(rIdx,cIdx);

% cla;imagesc([bw I2]);
