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
end

% Gather all the letters
for i=1:size(bbl,1)
    letter{i}=single(imcrop(bw_lets,bbl(i,:)));
%        imshow(letter{i})
%     i
%     letter_type{i}
end

fprintf('Loading background images...\n');
% for n=1:length(names)
%     fName = names(n).name;
%     I_list{n} = im2single(imread([dataPath fName]));
% end
%%

fprintf('Running...\n');
for i=1:num2gen
    %% GENERATE DATA
   num2gen-i
    % Create the shape crop
    indS = randi(length(shape));
    crop = shape{indS};
    cropsz = 128;
    cropscale = cropsz/max(size(crop));
    c = bbs_cent(indS,:)*cropscale;
    incircleRadius = bbs_radius(indS)*cropscale;
    crop=imresize(crop,cropscale,'nearest')*1;
    [hc wc] = size(crop);
    
  
    if rand()>0.5
        crop =  im2bw(imfilter( uint8(crop)*255, fspecial('gauss',[ceil(rand()*20) ceil(rand()*20)], round(max(rand()*20*10*0.3*(2.5-1)+.8,1)))),0.1);
    end
    
    
    % Create the letter crop and rotate it
    indL = randi(length(letter));
    let = letter{indL};
    letsz = (incircleRadius*2)/sqrt(2);
    letscale = letsz/max(size(let));
    let=imresize(let,letscale,'nearest');
    ang(i) = rand(1)*MaxAng;
    let = imrotate(let,ang(i));
    [hl wl] = size(let);
    
    if rand()>0.5
        let =  im2bw(imfilter(uint8(let)*255, fspecial('gauss',[ceil(rand()*5) ceil(rand()*5)], round(max(rand()*50*0.3*(2.5-1)+.8,1)))),0.1);
    end
  
    
    % Get all inds for the letter box
    [lColInds,lRowInds] = meshgrid(1:wl,1:hl);
    lInds = sub2ind(size(let),lRowInds(:),lColInds(:));
    
    % Get all inds for the letter box in the shape box around the
    % incircle center
    sRowInds = round(c(2)-hl/2) + lRowInds;
    sRowInds(sRowInds>hc)=hc;sRowInds(sRowInds<1)=1;
    sColsInds = round(c(1)-wl/2) + lColInds;
    sColsInds(sColsInds>wc)=wc;sColsInds(sColsInds<1)=1;
    sInds = sub2ind(size(crop),sRowInds(:),sColsInds(:));
    
    % Set the crop to the shape and give the shape the index 1
    im = uint8(crop)*1;
    
    % Now find the nonzero letter locations and set these locations in the
    % crop and give them the index 2
    nzLetInds = let(lInds)==1;
    im(sInds(nzLetInds))=2;
    
    im=double(im);
    imCol = im;
    colbgIdx = randi(length(colors));
    colfgIdx = randi(length(colors));
    colsgIdx = randi(length(colors));
    colbg = repmat(reshape(colors(colbgIdx,:),1,1,3),size(im,1),size(im,2));
    colfg = repmat(reshape(colors(colfgIdx,:),1,1,3),size(im,1),size(im,2));
    colsg = repmat(reshape(colors(colsgIdx,:),1,1,3),size(im,1),size(im,2));
    %imCol=cat(3,imCol,imCol,imCol);
%     imCol(imCol==1)=colbg(imCol==1);
%     imCol(imCol==2)=colfg(imCol==2);
    %imCol(imCol==0)=colsg(imCol==0);
    % Perform a random perspective transform
    imCol = genTform(imCol, MaxAng, MaxTrans ,ScaleLims,MaxShear);
    im_label = imCol;
    
    im_bg = 0*imCol(:,:,1);
    im_shape =im_bg;
    im_letter = im_bg;
    
    im_bg(imCol(:,:,1)==0)=1;
    im_shape(imCol(:,:,1)==1)=1;
    im_letter(imCol(:,:,1)==2)=1;
    
    % Extract background
%     imInd = randi(length(I_list));
%     I = I_list{imInd};
%     p=rand(1,2);
%     ir = round(1*p(1) + (1-p(1))*(size(I,1)-size(imCol,1)));
%     ic = round(1*p(2) + (1-p(2))*(size(I,2)-size(imCol,2)));
%     bg = I(ir+(1:size(imCol,1)), ic+(1:size(imCol,2)),:);
%     
%     %Get bg mask
%     mask = all(imCol==0,3);
%     mask = cat(3,mask,mask,mask);
%     
%     %Replace background with bg image
%     imCol(mask==1)=bg(mask==1);
%     % Do a bit of color blending for realism
%     imCol = imCol*alpha^2+bg*(1-alpha);
    
%% Useful output data
    
% figure(1);cla;imagesc(im_label);
OUTPUT.IMAGE_LABEL = im_label;
OUTPUT.SHAPE_IMAGE = im_shape;
OUTPUT.SHAPE_STR = bbs_type{indS};
OUTPUT.SHAPE_INDEX = indS;
OUTPUT.LETTER_INDEX = indL;
OUTPUT.LETTER_IMAGE = im_letter;
%    SHAPE.SHAPE_FULL = (im_shape+im_letter);
%     SHAPE.SHAPE_FULL_FILTER=im_shape_full_filter;
%     SHAPE.SHAPE_IMAGE = im_shape;
%     SHAPE.SHAPE_IMAGE_FILTER = im_shape_filter;
%     SHAPE.SHAPE_IMAGE_LETTER = im_shape_filter-im_letter_filter;
%     SHAPE.SHAPE_IMAGE_LETTER(SHAPE.SHAPE_IMAGE_LETTER<0)=0;
%     SHAPE.SHAPE_INDEX = indS;
%     SHAPE.SHAPE_STR = bbs_type{indS};
%     LETTER.LETTER_INDEX = indL;
%     LETTER.LETTER_IMAGE = im_letter;
%     LETTER.LETTER_IMAGE_FILTER = im_letter_filter;        
%     imshow(OUTPUT.SHAPE_FULL)
%     imshow(OUTPUT.SHAPE_IMAGE)
 %     imshow(OUTPUT.LETTER_IMAGE)
  save(['Outputs\S_' OUTPUT.SHAPE_STR '_' num2str(i) '.mat'],'OUTPUT');
%     save(['Shapes\S_' SHAPE.SHAPE_STR '_' num2str(i) '.mat'],'SHAPE');
%     save(['Letters\L_' num2str(indL) '_' num2str(i) '.mat'],'LETTER');
 %     imwrite(OUTPUT.SHAPE_IMAGE,['Shapes\' OUTPUT.SHAPE_STR '_' num2str(i) '.jpg']);
%     imwrite(OUTPUT.SHAPE_FULL,['FullShapes\' OUTPUT.SHAPE_STR '_' num2str(i) '.jpg']);
%     imwrite(OUTPUT.LETTER_IMAGE,['Letters\' num2str(indL) '_' num2str(i) '.jpg']);
%     if writeFiles
%     imwrite(OUTPUT.COLOR,[outPath sprintf('COL_%i_%i_%i.jpg',OUTPUT.SHAPE,OUTPUT.LETTER,i)]);
%     imwrite(OUTPUT.SEGMENTS,[outPath sprintf('GT_%i_%i_%i.tif',OUTPUT.SHAPE,OUTPUT.LETTER,i)]);
%     end
    
    %% DISPLAY
%     cla
%     imshow(imCol);
%     drawnow;
end




