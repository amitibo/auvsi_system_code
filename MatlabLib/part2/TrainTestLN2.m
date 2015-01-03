
function TrainTestLN2()

close all;
clc;

set(0,'DefaultFigureWindowStyle','docked');

train_path ='C:\Users\itayguy\Documents\Technion\UAS\data\GenPositives\';
svm_path   = 'C:\AUVSI\svm-files\';

[svmCoeff] = ReadInitFiles();
global M;
M=svmCoeff;
area_limit=M.data(1,10);

% rmdir([output_path],'s');
% mkdir([output_path]);
% rmdir([input_path],'s');
% mkdir([input_path]);

L_dir=dir([train_path 'Letters\*.mat']);
S_dir=dir([train_path 'Shapes\*.mat']);
N_dir=dir([train_path 'None\*.mat']);

L_prec = 90;
S_prec = 90;
N_prec = 50;

num_L_files = floor(length(L_dir)*L_prec/100);
num_S_files = floor(length(S_dir)*S_prec/100);
num_N_files = floor(length(N_dir)*N_prec/100);

num_LSN_files = num_L_files+num_S_files+num_N_files;

L_permute = randperm(length(L_dir));
S_permute = randperm(length(S_dir));
N_permute = randperm(length(N_dir));

LSN_permute = randperm(num_LSN_files);

L_vec_files =L_permute(1:num_L_files);
S_vec_files =S_permute(1:num_S_files);
N_vec_files =N_permute(1:num_N_files);


features=zeros(num_LSN_files,12);
group=cell(num_LSN_files,1);

L_cnt=0;S_cnt=0;N_cnt=0;f_index=0;
k=0;
for i=1:1:num_L_files
    
    group_name= 'L';
    
    k=k+1;
    num_LSN_files-k
       
    L_cnt=L_cnt+1;
    file_name=[train_path 'Letters\' L_dir(L_vec_files(L_cnt)).name];
    load(file_name);
    
    
    I=LETTER.LETTER_IMAGE;
    
    f_vec = FeaturesLSN(uint8(I));
    f_index=f_index+1;
    features(f_index,:)=f_vec;
    group{f_index} =group_name;
    
    I=LETTER.LETTER_IMAGE_FILTER;
    f_vec = FeaturesLSN(uint8(I));
    f_index=f_index+1;
    features(f_index,:)=f_vec;
    group{f_index} =group_name;
end

for i=1:1:num_S_files
    group_name= 'N';
    
    k=k+1;
    num_LSN_files-k
    
    S_cnt=S_cnt+1;
    file_name=[train_path 'Shapes\' S_dir(S_vec_files(S_cnt)).name];
    load(file_name);
    
    
    I=SHAPE.SHAPE_FULL;
    f_vec = FeaturesLSN(uint8(I));
    f_index=f_index+1;
    features(f_index,:)=f_vec;
    group{f_index} =group_name;
    I=SHAPE.SHAPE_IMAGE_FILTER;
    f_vec = FeaturesLSN(uint8(I));
    f_index=f_index+1;
    features(f_index,:)=f_vec;
    group{f_index} =group_name;
    I=SHAPE.SHAPE_IMAGE;
    f_vec = FeaturesLSN(uint8(I));
    f_index=f_index+1;
    features(f_index,:)=f_vec;
    group{f_index} =group_name;
    I=SHAPE.SHAPE_IMAGE_LETTER;
    f_vec = FeaturesLSN(uint8(I));
    f_index=f_index+1;
    features(f_index,:)=f_vec;
    group{f_index} =group_name;
    
end

se = strel('disk',2);

for i=1:1:num_N_files
    k=k+1;
     num_LSN_files-k
    group_name= 'N';
    N_cnt=N_cnt+1;
    file_name=[train_path 'None\' N_dir(N_vec_files(N_cnt)).name];
    load(file_name);
    for g=1:length(NONE.IMAGE)
    I=NONE.IMAGE{g};
%     figure(1);cla;imagesc(uint8(I));
%     I = imopen(I,se);
    if isempty(find(I>0,1))
        continue;
    end
%     figure(1);cla;imagesc(uint8(I));
    f_vec = FeaturesLSN(uint8(I));
    f_index=f_index+1;
    features(f_index,:)=f_vec;
    group{f_index} =group_name;
    end
    
end

%save data
save ([svm_path 'lnBuild.mat'], 'features' ,'group','L_vec_files','S_vec_files','N_vec_files');
%training
tic
%kernel_function = 'polynomial';
kernel_function = 'rbf';
options = statset('MaxIter',30000,'Display','iter');
lnStruct = svmtrain(features,group,'kernel_function',kernel_function,'options',options);
toc
save ([svm_path 'lnStruct.mat'], 'lnStruct')

end