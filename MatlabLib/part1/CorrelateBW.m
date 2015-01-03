%calc correlation between two bw images
function corr = CorrelateBW(BW1,BW2)

BW1_num_pixel = sum(sum(BW1));
BW2_num_pixel = sum(sum(BW2));

if BW1_num_pixel==0 && BW2_num_pixel==0
    corr=inf;
else
    corr = sum(sum(abs(BW1-BW2)))/(BW1_num_pixel+BW2_num_pixel);
end
