function [sign_bw,sign_color] = GetSignUsingLetter(I,I_deleted,letter_bw)

global M
area_limit=M.data(1,10);

pixel_labels = KmeanFast(I_deleted,'rgb',2);
% [pixel_labels] = KmeanSeg(I_deleted,'rgb',2,2);
% [pixel_labels] = KmeanFastSegABC(I_deleted,'rgb',2);

properties{1}='Centroid';
properties{2}='Area';
properties{3}='MajorAxisLength';
properties{4}='MinorAxisLength';
properties{5}='PixelIdxList';

STATS = regionprops(pixel_labels,properties);

region1_grade = (STATS(1).MajorAxisLength)^2+ (STATS(1).MinorAxisLength)^2;
region2_grade = (STATS(2).MajorAxisLength)^2+ (STATS(2).MinorAxisLength)^2;

temp_zero=0*pixel_labels;
sign_bw=temp_zero;

if region1_grade<region2_grade
    sign_bw(STATS(1).PixelIdxList)=1;
else
    sign_bw(STATS(2).PixelIdxList)=1;
end

%remove noise
se = strel('disk',3);
sign_bw = imopen(sign_bw,se);
sign_bw = bwareaopen(sign_bw, 3*area_limit, 8);

properties=[];
properties{1}='Area';
properties{2}='PixelIdxList';
STATS = regionprops(sign_bw,properties);
if length(STATS)>1
    area_vec=zeros(length(STATS),1);
    for i=1:length(STATS)
        area_vec(i)=STATS(i).Area;
    end
    [~,ix]=max(area_vec);
    ind_sign_bw=STATS(ix).PixelIdxList;
    sign_bw=temp_zero;
    sign_bw(ind_sign_bw)=1;

end

sign_bw_neto = double(xor(sign_bw,letter_bw));

%make letter smaller
se = strel('disk',3);
sign_bw_neto_erode = imerode(sign_bw_neto,se);
ind_sign_bw=find(sign_bw_neto_erode);
num_sign_bw = length(ind_sign_bw);
%if we erased it totaly we use the the org
if num_sign_bw==0
  ind_sign_bw=find(sign_bw_neto);
  num_sign_bw = length(ind_sign_bw);
end

I=double(I);
R_layer = I(:,:,1);
G_layer = I(:,:,2);
B_layer = I(:,:,3);

R_color_sign =sum(R_layer(ind_sign_bw))/num_sign_bw;
G_color_sign =sum(G_layer(ind_sign_bw))/num_sign_bw;
B_color_sign =sum(B_layer(ind_sign_bw))/num_sign_bw;
sign_color=[R_color_sign G_color_sign B_color_sign];


