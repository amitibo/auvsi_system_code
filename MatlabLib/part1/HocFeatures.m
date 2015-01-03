%this function calcs Histogram of Colors , first it use kmean to transform
%the image to 8 colors , then it calcs the color histogram in each cell,
%and creates a vector out of them
function f_vec=HocFeatures(I)

global color_model;
pixel_labels = KmeanFast(I,color_model,8);
f_vec =Hoc(pixel_labels);
end

function f_vec =Hoc(pixel_labels)
global M;
num_rows=M.data(1,2);
num_cols=M.data(1,3);

num_colors=8;
pixel_cell = 13;

c_max_divider=8;
r_max_divider=8;
bins=1:num_colors;
f_vec=zeros(1,1152);
f_cnt=0;
for i=1:c_max_divider:(num_cols-c_max_divider-1)
    for j=1:r_max_divider:(num_rows-r_max_divider-1)
        temp_cell = pixel_labels(i:i+pixel_cell-1,j:j+pixel_cell-1);
        temp_vec = reshape(temp_cell,(pixel_cell*pixel_cell),1);
        [counts]=histc(temp_vec,bins)/(pixel_cell*pixel_cell);
        
        f_vec(f_cnt*num_colors+1:(f_cnt+1)*num_colors)=counts';
        f_cnt=f_cnt+1;
    end
end

end