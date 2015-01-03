function GenNone()

close all;
clc;

set(0,'DefaultFigureWindowStyle','docked');

crop_path = 'C:\Users\itayguy\Documents\Technion\UAS\data\run6\crops\';
to_path = 'C:\Users\itayguy\Documents\Technion\UAS\data\GenPositives\None\';
[svmCoeff] = ReadInitFiles();

rmdir([to_path],'s');
mkdir([to_path]);

global NONE
NONE.IMAGE={};
NONE.NAME={};
NONE.cnt=0;

global M
M=svmCoeff;
global plot_mode
nColors=M.data(1,1);
num_rows=M.data(1,2);
num_cols=M.data(1,3);
std_limit=M.data(1,4);
area_limit=M.data(1,10);

D_loc_N=M.D_loc_N;
Y_loc_N=M.Y_loc_N;
X_loc_N=M.X_loc_N;

crop_dir=dir([crop_path '*.jpg']);


num_y_files=length(crop_dir);

%shuffle inputs
k=0;
for i=1:1:5000
    k=k+1;
    500-i
    
    file_name = crop_dir(k).name;
    file_name_neto = file_name(1:end-4);
    full_file_name=[crop_path file_name];
    
    I=imread(full_file_name);
    I = imresize(I, [num_rows num_cols]);
    [I_edge,I_gray,I_gray_gauss]  = CombineEdge(I);
    if std2(I_gray)<std_limit
      continue;  
    end
    %figure(1);cla;imagesc(I)
    [pixel_labels] =KmeanSegABC(I,D_loc_N,Y_loc_N,X_loc_N,'lab',4,3);
    SaveLabels(pixel_labels,to_path,[file_name_neto '_KmeanSegABC_4_3']);
    %figure(2);cla;imagesc(pixel_labels)
    [pixel_labels] =KmeanFastSegABC(I,D_loc_N,Y_loc_N,X_loc_N,'lab',7);
    SaveLabels(pixel_labels,to_path,[file_name_neto '_KmeanFastSegABC_7']);
    %figure(3);cla;imagesc(pixel_labels)
    [pixel_labels] =KmeanFastSegABC(I,D_loc_N,Y_loc_N,X_loc_N,'lab',2);
    SaveLabels(pixel_labels,to_path,[file_name_neto '_KmeanFastSegABC_2']);
    %figure(4);cla;imagesc(pixel_labels)
    bandWidth=11;
    [pixel_labels] = MeanShiftABC(I,D_loc_N,Y_loc_N,X_loc_N,'lab',bandWidth);
    SaveLabels(pixel_labels,to_path,[file_name_neto '_MeanShiftABC_11']);
    %figure(5);cla;imagesc(pixel_labels)
    bandWidth=15;
    [pixel_labels] = MeanShiftABC(I,D_loc_N,Y_loc_N,X_loc_N,'lab',bandWidth);
    SaveLabels(pixel_labels,to_path,[file_name_neto '_MeanShiftABC_15']);
    %figure(6);cla;imagesc(pixel_labels)
    
    
    %figure(9);cla;imagesc(I_edge)
    maxWidth=15;
    [swtIn swtccIn] = SwtLight( I_gray_gauss,I_edge, 1, maxWidth );
    SaveLabels(swtccIn,to_path,[file_name_neto '_SwtLight_1_15']);
    %figure(7);cla;imagesc(swtccIn)
    [swtIn swtccIn] = SwtLight( I_gray_gauss,I_edge, 0, maxWidth );
    SaveLabels(swtccIn,to_path,[file_name_neto '_SwtLight_0_15']);
    %figure(8);cla;imagesc(swtccIn)
%   
%     if mod(k,100)==0
%         save NONE.mat NONE
%     end
end

end

function SaveLabels(pixel_labels,to_path,name)

if isempty(pixel_labels)
        return;
end
    
global NONE
global M
area_limit=M.data(1,10);
num_lables = max(max(pixel_labels));

for i=1:num_lables
    temp_bw=0*pixel_labels;
    temp_bw(pixel_labels==i)=1;
    CC = bwconncomp(temp_bw);
    for j=1:CC.NumObjects
        temp_cc=0*temp_bw;
        temp_cc(CC.PixelIdxList{j})=1;
        if sum(sum(temp_cc))>=area_limit
        NONE.cnt=NONE.cnt+1;
        NONE.IMAGE{j}=logical(temp_cc);
               
%         imwrite(uint8(temp_cc),[to_path name '_' num2str(j) '.gif']);
%         imshow(uint8(temp_cc));
%         NONE.cnt=NONE.cnt+1;
%         NONE.IMAGE{NONE.cnt}=logical(temp_cc);
%         NONE.NAME{NONE.cnt}=[name '_' num2str(j)];
        end
        NONE.NAME=[name '_' num2str(i)];
        save( [to_path name '_' NONE.NAME '.mat'],'NONE');
    end
    
end

end




