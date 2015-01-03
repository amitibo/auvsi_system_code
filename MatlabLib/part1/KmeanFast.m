
function pixel_labels = KmeanFast(abc_he,type,nColors)

switch(type)
    case 'lab'
        cform = makecform('srgb2lab');
        abc_he = applycform(abc_he(:,:,1:3),cform);
    case 'hsv'
        abc_he = rgb2hsv(abc_he(:,:,1:3));
    case 'rgb'
        abc_he = (abc_he(:,:,1:3));
end

ncols= size(abc_he,1);
nrows= size(abc_he,2);
ndim= size(abc_he,3);
re_abc = reshape(double(abc_he),nrows*ncols,ndim);

[cluster_idx ] = kmeansK(re_abc,nColors);
cluster_idx=double(cluster_idx);
pixel_labels = reshape(cluster_idx,nrows,ncols);


end