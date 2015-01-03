function [im, T, t_proj] = genTform(I,ang, trans,scaling,shear, allowCropping)

if ~exist('allowCropping')
    allowCropping=false;
end

T=rand(3,3)-0.5;
theta = (rand(1)-0.5)*2*ang;
%[u,s,v]=svd(T(1:2,1:2));
sc=rand(1);
sc=scaling(1)*sc+(1-sc)*scaling(2); % Scaling
T(1:2,1:2)=[cosd(theta) sind(theta);-sind(theta) cosd(theta)]*sc; % Pure rotation
T(2,1)=T(2,1)+rand(1)*shear; % Ammount of shear
T(1:2,3)=(T(1:2,3))*1; % Ammount of perspective
T(3,1:2)=T(3,1:2)*trans; % Translation
T(9)=1;

t_proj = maketform('projective',T);

if ~allowCropping
    % This code ensures everything is inside the final canvas. No cropping
    im = imtransform(I,t_proj,'nearest','FillValues',0,...
        'size',size(I),...
        'UData',[-0.5 0.5], 'VData',[-0.5 0.5]);% This assumes square images...
    
    % For this case scaling and translation dont have the same meaning and
    % we need to add an additional phase to guarantee no cropping.
    % We can do this using padarray. This wont be part of the final transfrom t_proj
    [h,w,~] = size(im);
    t = rand(1,2)*trans/2; % again randomize translation
    if sc>1, sc=1; end
    dw = w/sc-w; % Set zeros for padding each side to rescale
    dh = h/sc-h;
    
    im = padarray(im,round([dh dw].*(0.5+t)),'pre');
    im = padarray(im,round([dh dw].*(0.5+(1-t))),'post');
else
    % This code allows shapes to be projected to outside of the canvas area
    im = imtransform(I,t_proj,'nearest','FillValues',0,...
        'size',size(I),...
        'XData',[-0.5 0.5], 'YData',[-0.5 0.5],... % This assumes square images...
        'UData',[-0.5 0.5], 'VData',[-0.5 0.5]);
end

