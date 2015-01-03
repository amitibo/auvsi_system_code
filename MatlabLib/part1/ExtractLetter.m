%this function uses invariant moments to find a letter from a bw image , it
%test all the elements and find which it thinks might be the best match for
%a letter.

function [letter_bw,grade]= ExtractLetter(pixel_labels)

%global nFeaturesLSN
global M
global N
global svm_grade;
svm_grade=NaN;
L_grade=[];
L_index=0;

area_limit=M.data(1,10);

lnStruct = N.lnStruct;

num_of_labels = double(max(max(pixel_labels)));

%the vector features that is used for the svm is made of:
properties{1}='Centroid';
properties{2}='Area';
properties{3}='MajorAxisLength';
properties{4}='MinorAxisLength';
properties{5}='Eccentricity';
properties{6}='EulerNumber';
properties{7}='Solidity' ;
properties{8}='Extent';
properties{9}='ConvexArea';
properties{10}='Perimeter';
properties{11}='PixelIdxList';


temp_zero=logical(0*pixel_labels);
%se = strel('disk',2);

region_cnt=0;
sum_num_of_components=0;
for k = 1:num_of_labels
    
    temp_bw = temp_zero;
    temp_bw(pixel_labels==k)= 1;
    %remoe small noise 
    temp_bw = bwareaopen(temp_bw, area_limit, 8);
    %ropen not good need to leave it for good calssification
    STATS = regionprops(temp_bw,properties);
    num_of_components=length(STATS);
    if num_of_components>0
        sum_num_of_components=sum_num_of_components+num_of_components;
        region_cnt=region_cnt+1;
        REGION(region_cnt).STATS=STATS;
        REGION(region_cnt).num_of_components=num_of_components;
        
    end
end


%for each region 
num_real_regions=0;
for j=1:region_cnt
    STATS=REGION(j).STATS;
    num_of_components=REGION(j).num_of_components;
    for i=1:(num_of_components)
        if STATS(i).Area > area_limit
            num_real_regions=num_real_regions+1;
            %build region
            temp_region =temp_zero;
            temp_region(STATS(i).PixelIdxList)=1;
            %figure(10);cla;imagesc(uint8(temp_region))
            f_vec = FeaturesLSN(temp_region,STATS(i));
            ln_type = svmclassify(lnStruct,f_vec);
            svm_grade;
            if strcmp(ln_type,'L')
                L_index=L_index+1;
                L_grade(L_index)=[svm_grade];
                L_stats_i(L_index)=[i];
                L_region_j(L_index)=[j];
            end
        end
    end
end

if L_index>0
    [grade,ix]=max(L_grade);
    temp_zero(REGION(L_region_j(ix)).STATS(L_stats_i(ix)).PixelIdxList)=1;
    letter_bw =temp_zero;
else
    letter_bw=temp_zero;
    grade=-inf;
end


end
