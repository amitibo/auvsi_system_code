function TrainTestFeatures(test_or_train)
if nargin <1
    test_or_train='train';
end

close all;
clc;

set(0,'DefaultFigureWindowStyle','docked');

% train_path ='C:\Users\itayguy\Documents\Technion\UAS\data\run6\hist2\crops\y\';% 'C:\Users\itayguy\Documents\Technion\UAS\data\run2\train\';
% train_y_path=[train_path 'y\'];
% train_n_path=[train_path 'n\'];

train_y_path ='C:\Users\itayguy\Documents\Technion\UAS\data\GenPositives\Outputs\';
train_n_path = 'C:\Users\itayguy\Documents\Technion\UAS\data\eli\train\n\';

input_path = 'C:\AUVSI\mser-inputs\';
output_path= 'C:\AUVSI\segmentation-ouputs\';
svm_path   = 'C:\AUVSI\svm-files\';
result_path = 'C:\AUVSI\svm-results\';

if (strcmp(test_or_train,'test'))
    load ([svm_path 'svmBuild.mat']);
    [coeff_data,mat_data]  = ReadInitFiles();
else
    [coeff_data,mat_data] = ReadInitFiles();
end

global learn_mode
learn_mode=false;

global color_model
color_model='lab';

global features_mode
features_mode='kmeans3';%'hist2';%'hist';%'swt';%'kmeans'

global nFeatures
switch (features_mode)
    case 'swt'
        nFeatures = 1956;%1964;
        kernel_function = 'quadratic';
    case 'kmeans'
        nFeatures = 1205;
        kernel_function = 'quadratic';
    case 'hist'
        nFeatures = 1945;
        kernel_function = 'quadratic';
    case 'hist2'
        nFeatures = 2329;
        kernel_function = 'quadratic';
    case 'kmeans3'
        nFeatures = 15;
        kernel_function = 'quadratic';
        
        global N
        N = mat_data ;
        svmStruct = N.svmStruct;
        global nFeaturesLSN
        nFeaturesLSN=11;
        
        global M
        M=coeff_data;
        
        num_rows=M.data(1,2);
        num_cols=M.data(1,3);
        
        
end

global plot_mode
plot_mode=0;


rmdir([output_path],'s');
mkdir([output_path]);
rmdir([input_path],'s');
mkdir([input_path]);



if (strcmp(test_or_train,'train'))
    
    switch (features_mode)
        case 'kmeans3'
            y_dir=dir([train_y_path '*.mat']);
        otherwise
            y_dir=dir([train_y_path '*.jpg']);
    end
    
    n_dir=dir([train_n_path '*.jpg']);
    
    y_prec = 50;
    n_prec = 50;
    
    num_y_files = floor(length(y_dir)*y_prec/100);
    num_n_files = floor(length(n_dir)*n_prec/100);
    num_ny_files = num_n_files+num_y_files;
    
    y_permute = randperm(length(y_dir));
    n_permute = randperm(length(n_dir));
    ny_permute = randperm(num_ny_files);
    
    y_vec_files =y_permute(1:num_y_files);
    n_vec_files =n_permute(1:num_n_files);
else
    rmdir([result_path 'fp\true'],'s');
    mkdir([result_path 'fp\true']);
    rmdir([result_path 'fp\false'],'s');
    mkdir([result_path 'fp\false']);
    rmdir([result_path 'fn\true'],'s');
    mkdir([result_path 'fn\true']);
    rmdir([result_path 'fn\false'],'s');
    mkdir([result_path 'fn\false']);
    
    
    switch (features_mode)
        case 'kmeans3'
            y_dir=dir([train_y_path '*.mat']);
        otherwise
            y_dir=dir([train_y_path '*.jpg']);
    end
    
    n_dir=dir([train_n_path '*.jpg']);
    
    y_prec = 10;
    n_prec = 10;
    
    num_y_files = floor(length(y_dir)*y_prec/100);
    num_n_files = floor(length(n_dir)*n_prec/100);
    num_ny_files = num_n_files+num_y_files;
    
    y_permute = randperm(length(y_dir));
    n_permute = randperm(length(n_dir));
    
    y_permute(y_vec_files)=0;
    n_permute(n_vec_files)=0;
    
    temp_y_permute = y_permute(y_permute>0);
    temp_n_permute = n_permute(n_permute>0);
    
    y_vec_files =temp_y_permute(1:num_y_files);
    n_vec_files =temp_n_permute(1:num_n_files);
    
end

if (strcmp(test_or_train,'train'))
    global features;
    features=zeros(num_ny_files,nFeatures);
    
    global group
    group=cell(num_ny_files,1);
    
    global f_index;
    f_index=0;
end

group_name=[];
y_cnt=0;
n_cnt=0;
LL=[];
NN=[];

if (strcmp(test_or_train,'train'))
    for i=1:1:num_ny_files
        num_ny_files-i
        
        %shuffle inputs
        if ny_permute(i)<=num_y_files
            y_cnt=y_cnt+1;
            file_name=[train_y_path y_dir(y_vec_files(y_cnt)).name];
            group_name= 'L';
        else
            n_cnt=n_cnt+1;
            file_name=[train_n_path n_dir(n_vec_files(n_cnt)).name];
            group_name= 'N';
        end
        %file_name='C:\Users\itayguy\Documents\Technion\UAS\data\run2\train\y\n_154_crop_7.jpg';
        %f_vec=TestSpatialFeatures(file_name,svmCoeff);
        
        
        
        switch (features_mode)
            case 'kmeans3'
             
                if  strcmp(group_name,'N');
                    I=imread(file_name);
                else
                    load(file_name);
                    I=OUTPUT.IMAGE_LABEL;
                    I=cat(3,I,I,I);
                end
                I = imresize(I, [num_rows num_cols]);
                [f_vec] = FeaturesSpatial(I);
            otherwise
                f_vec=AnalyzeCrop(file_name,svmCoeff,svmStruct);
        end
        
        
        f_index=f_index+1;
        features(f_index,:)=f_vec;
        %group{f_index} =sscanf(d(i).name,'%c1');
        group{f_index} =group_name;
    end
else
    
    switch (features_mode)
        case 'kmeans3'
            if  strcmp(group_name,'N');
                I=imread(file_name);
                I = imresize(I, [num_rows num_cols]);
                [f_vec] = FeaturesSpatial(I);
            else
                load(file_name);
                I=OUTPUT.IMAGE_LABEL;
                %I=cat(3,I,I,I);
                rp=randperm(3);
                I(I==1)=rp(1)+3;
                I(I==2)=rp(2)+3;
                I(I==3)=rp(3)+3;
                I = imresize(I, [num_rows num_cols]);
                
                learn_mode=true;
                [f_vec] = FeaturesSpatial(I);
                learn_mode=false;
            end
            
        otherwise
            f_vec=AnalyzeCrop(file_name,svmCoeff,svmStruct);
    end

%shuffle inputs
for i=1:1:num_y_files
    num_y_files-i
    y_cnt=y_cnt+1;
    file_name = y_dir(y_vec_files(y_cnt)).name;
    full_file_name=[train_path 'y\' y_dir(y_vec_files(y_cnt)).name];
    f_vec=TestSpatialFeatures(full_file_name,svmCoeff);
    group = svmclassify(svmStruct,f_vec);
    if strcmp(group,'L')
        LL(y_cnt)=1;
    else
        I=imread(full_file_name);
        
        figure(10);cla;imagesc(I);title('LL');
        yn = input('is it a target(y/n)?:','s');
        if strcmp(yn,'n')
            LL(y_cnt)=1;
            status =copyfile(full_file_name,[result_path '\fp\true\' file_name]);
        else
            LL(y_cnt)=0;
            status =copyfile(full_file_name,[result_path '\fp\false\' file_name]);
        end
        %             plot_mode=1;
        %             f_vec=TestSpatialFeatures(file_name,svmCoeff);
        %             plot_mode=0;
        
    end
end

for i=1:1:num_n_files
    num_n_files-i
    n_cnt=n_cnt+1;
    file_name=n_dir(n_vec_files(n_cnt)).name;
    full_file_name=[train_path 'n\' n_dir(n_vec_files(n_cnt)).name];
    f_vec=TestSpatialFeatures(full_file_name,svmCoeff);
    group = svmclassify(svmStruct,f_vec);
    if strcmp(group,'N')
        NN(n_cnt)=1;
    else
        I=imread(full_file_name);
        
        figure(10);cla;imagesc(I);title('NN');
        
        yn = input('is it a target(y/n)?:','s');
        if strcmp(yn,'n')
            NN(n_cnt)=0;
            status =copyfile(full_file_name,[result_path '\fn\true\' file_name]);
        else
            NN(n_cnt)=1;
            status =copyfile(full_file_name,[result_path '\fn\false\' file_name]);
        end
        %             plot_mode=1;
        %             f_vec=TestSpatialFeatures(file_name,svmCoeff);
        %             plot_mode=0;
    end
end
disp(['false positive:' num2str(100*sum((LL==0))/length(LL))]);
disp(['false negative:' num2str(100*sum((NN==0))/length(NN))]);
results.fp = 100*sum((LL==0))/length(LL);
results.fn = 100*sum((NN==0))/length(NN);
end
%d(i).name

%input('s')

if (strcmp(test_or_train,'train'))
    switch (features_mode)
        case 'kmeans3'
            save ([svm_path 'kmeans3Build.mat'], 'features' ,'group','y_vec_files','n_vec_files');
            
            tic
            %kernel_function = 'polynomial';
            %kernel_function = 'rbf';
            kernel_function = 'linear';
            %kernel_function = 'quadratic';
                        
            options = statset('MaxIter',30000,'Display','iter');
            kmeans3Struct = svmtrain(features,group,'kernel_function',kernel_function,'options',options);
            toc
            svm_path   = 'C:\AUVSI\svm-files\';
            save ([svm_path 'kmeans3Struct.mat'], 'kmeans3Struct')
            
        otherwise
            
            save ([svm_path 'SvmBuild.mat'], 'features' ,'group','y_vec_files','n_vec_files');
            svmStruct = svmtrain(features,group,'kernel_function',kernel_function);
            svm_path   = 'C:\AUVSI\svm-files\';
            save ([svm_path 'svmStruct.mat'], 'svmStruct')
    end
else
    save ([result_path 'results.mat'], 'LL','NN')
end

end

