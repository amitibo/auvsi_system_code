%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
dataPath='Images/'; % Path to background images
outPath = 'targets/';
num2gen=3000; % Number of images to synth
MaxAng = 180; % Maximum angle to rotate the crops
MaxTrans = 0.1; % Maximum ammount to shift crop images i.e. 10% inside the frame
ScaleLims = [0.96 0.99]; % Scaling lims
MaxShear = 0.05; % Maximum shearing
alpha = 0.8; %background foreground color blending coefficient
writeFiles = false;

% Color pallette
colors = hsv(62);
colors = [colors; 0.99 0.99 0.99; 0.01 0.01 0.01];

% rmdir(['.\Outputs\'],'s');
% mkdir(['.\Outputs\']);
% rmdir(['.\Shapes\'],'s');
% mkdir(['.\Shapes\']);
% rmdir(['.\FullShapes\'],'s');
% mkdir(['.\FullShapes\']);
% rmdir(['.\Letters\'],'s');
% mkdir(['.\Letters\']);

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%% Extracting shapes and letters
names = dir([dataPath '*.jpg']);
fprintf('Extracting letters and shapes...\n');

bw_shapes=im2single(rgb2gray(imread('ShapesL.png')))<0.99;
bw_lets=im2single(rgb2gray(imread('PictureLets.png')))<0.5;

s  = regionprops(bw_shapes, 'BoundingBox');
bbs = cat(1, s.BoundingBox);
bbs(:,1:2)=bbs(:,1:2)+bbs(:,3:4)/2;
bbs(:,3:4)=max(bbs(:,3:4),[],2)*[1 1];
bbs(:,1:2)=bbs(:,1:2)-bbs(:,3:4)/2;

s  = regionprops(bw_lets, 'BoundingBox','PixelList');
bbl = cat(1, s.BoundingBox);
bbl(:,1:2)=bbl(:,1:2)+bbl(:,3:4)/2;
bbl(:,3:4)=max(bbl(:,3:4),[],2)*[1 1];
bbl(:,1:2)=bbl(:,1:2)-bbl(:,3:4)/2;

%% Gather all shapes and make sure the center of the shape is the incircle of
%the convex hull or at least that we have the closest radius
load bbs_type.mat
for i=1:size(bbs,1)
    shape{i}=single(imcrop(bw_shapes,bbs(i,:)));
    %  = bwboundaries(BW)
    [r ang]=getMinShapeRadius(shape{i});
    [y,x] = find(shape{i}); H = convhull(x,y);
    bbs_cent(i,:) = incircle([x,y],H);
    bbs_radius(i) = r;
    %imshow(shape{i})
    i
    bbs_type{i}
    
    crop = shape{i};
    cropsz = 128;
    cropscale = cropsz/max(size(crop));
    c = bbs_cent(i,:)*cropscale;
    incircleRadius = bbs_radius(i)*cropscale;
    crop=imresize(crop,cropscale,'nearest')*1;
    crop = padarray(crop, [10 10]);
    figure(19);cla;imagesc(crop);title(bbs_type{i});
     
%     C = contourc(double(crop));
%     s=contourdata(C);
   % bwtraceboundary;
%     [cx,cy] = contourmex (im2bw(crop));
   [signature,centroids]= ExtractBwSignature(crop);
   % [signature]= ExtractAffineBwSignature(crop);
    
    SHAPES_DB.crops{i}=crop;
    SHAPES_DB.signature{i}=signature;
    SHAPES_DB.bbs_type{i}=bbs_type{i};
    SHAPES_DB.centroid{i}=centroids;
    
    figure(18);plot(signature(:,1),signature(:,2));axis tight
end
save(['C:\AUVSI\svm-files\' 'SHAPES_DB.mat'],'SHAPES_DB');
