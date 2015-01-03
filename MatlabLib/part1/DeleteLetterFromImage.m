%return Image with the letter blurred into the same color of the sign based
%on the letter perimeter
function [I,letter_color]=DeleteLetterFromImage(I,letter_bw)


se = strel('disk',3);

dilate_bw = imdilate(letter_bw,se);
ind_dilate_bw=find(dilate_bw);

%perim_bw = double(xor(dilate_bw,letter_bw));
perim_bw = bwperim(dilate_bw, 8);
ind_perim_bw = find(perim_bw);
num_perim_bw =length(ind_perim_bw);
% num_perim_bw = sum(sum(perim_bw==1));

I=double(I);
R_layer = I(:,:,1);
G_layer = I(:,:,2);
B_layer = I(:,:,3);

%get sign color for delete
R_color_perim =sum(R_layer(ind_perim_bw))/num_perim_bw;
G_color_perim =sum(G_layer(ind_perim_bw))/num_perim_bw;
B_color_perim =sum(B_layer(ind_perim_bw))/num_perim_bw;

%make letter smaller
se = strel('disk',1);
letter_bw_erode = imerode(letter_bw,se);
ind_letter_bw=find(letter_bw_erode);
num_letter_bw = length(ind_letter_bw);

%if we erased it totaly we use the the org
if num_letter_bw==0
ind_letter_bw=find(letter_bw);
end
num_letter_bw = length(ind_letter_bw);

R_color_letter =sum(R_layer(ind_letter_bw))/num_letter_bw;
G_color_letter =sum(G_layer(ind_letter_bw))/num_letter_bw;
B_color_letter =sum(B_layer(ind_letter_bw))/num_letter_bw;

%erase letter
R_layer(ind_dilate_bw)=R_color_perim;
G_layer(ind_dilate_bw)=G_color_perim;
B_layer(ind_dilate_bw)=B_color_perim;

letter_color=[R_color_letter G_color_letter B_color_letter];

I=cat(3,R_layer,G_layer,B_layer);
I=uint8(I);
