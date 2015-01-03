
%this function calcs mean shift for an image based on 3 channels and
%distance from the center 
function [pixel_labels,cluster_center] = MeanShiftABC(he)

global color_model
color_model='lab';

global M
ABC_div=M.data(1,5);
dist_fac=M.data(1,7);

band_width=M.data(1,11);
D_loc_N=M.D_loc_N;


switch(color_model)
    case 'lab'
        cform = makecform('srgb2lab');
        abc_he = applycform(he(:,:,1:3),cform);
    case 'hsv'
        abc_he = rgb2hsv(he(:,:,1:3));
    case 'rgb'
        abc_he = (he(:,:,1:3));
end



abc = double(abc_he(:,:,:));
abc(:,:,1) = abc(:,:,1)/ABC_div;
abc(:,:,2) = abc(:,:,2)/ABC_div;
abc(:,:,3) = abc(:,:,3)/ABC_div;
abc(:,:,4) = D_loc_N*dist_fac;

nrows = size(abc,1);
ncols = size(abc,2);
ndim= size(abc,3);

re_abc = reshape(abc,nrows*ncols,ndim)';

[cluster_center,data2cluster] = MeanShiftCluster(re_abc,band_width,0);
pixel_labels = reshape(data2cluster,nrows,ncols);

end