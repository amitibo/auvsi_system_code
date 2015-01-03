function [I_edge,I_gray,I_gray_gauss] = CombineEdge(I)
I_gray=rgb2gray(I);
I_gray_gauss = imfilter( I_gray, fspecial('gauss',[3 3],2) );
I_range_fit_gray = rangefilt(I_gray);%, MySE('circle',4));

maxp=uint16(max(max(I_range_fit_gray)));
minp=uint16(min(min(I_range_fit_gray)));
I_rangefilt_edge=im2bw(I_range_fit_gray,(double(minp+maxp))/(2*255));
I_canny_edge = edge(I_gray_gauss,'canny');%,.15);
I_edge=or(I_canny_edge,I_rangefilt_edge);