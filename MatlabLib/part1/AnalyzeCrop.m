function [output,letter_color,sign_color,letter_alphanumeric,letter_orientation,sign_shape]=AnalyzeCrop(str,coeff_data,mat_data,yaw)

%main function to analyze crop and get it's parameters

%get crop name and path
token = strfind(str, '\');
file_name=str(token(end)+1:end);
file_name_neto = file_name(1:end-4);
file_path = str(1:token(end));

%yaw or aircraft
yaw=int32(yaw);

%libs for needed inputs
input_path  = 'C:\AUVSI\mser-inputs\';
output_path = 'C:\AUVSI\segmentation-ouputs\';


%global params
global N
N = mat_data ; %structs from init file

global M
M=coeff_data;  %coeff data from text file
global plot_mode
svm_limit=M.data(1,1);
num_rows=M.data(1,2);
num_cols=M.data(1,3);
std_limit=M.data(1,4);
plot_mode=1;%M.data(1,9);
BW_correlate_limit=M.data(1,13);
min_ocr_result=M.data(1,14);


global svm_grade;
svm_grade=0;

global features_mode
features_mode='hist2';

global nFeaturesLSN
nFeaturesLSN=11;


global color_model
color_model='lab';

if nargin <3
    learn_mode=true;
else
    learn_mode=false;
end

if plot_mode==1
    close all;
    set(0,'DefaultFigureWindowStyle','docked');
end

%copy files to my lib for debug later
if learn_mode==false
    status =copyfile(str,[input_path file_name]);
    if status==0
        disp(['copy of: ' str ' to ' [input_path file_name] ' failed']);
    end
end


%read crop
I=imread(str);
I = imresize(I, [num_rows num_cols]);
I_gray=rgb2gray(I);
I_std=std2(I_gray);

if plot_mode==true
    figure(1);cla;imagesc(I);title('ORG');
end

%init outputs
output=[0 0];
letter_alphanumeric='none';
letter_orientation='none';
sign_shape='none';
letter_color='none';
sign_color='none';


%blank check
if learn_mode==false
    if  I_std<std_limit%blank picture
        output=[0 0];
        csvwrite([output_path file_name '.dat'],[0 0]);
        letter_alphanumeric='std limit fail';
        letter_orientation='std limit fail';
        sign_shape='std limit fail';
        letter_color='std limit fail';
        sign_color='std limit fail';
        return;
    end
end

%implement several svm based on diffrent features
%calc the feature vector

%case 'hist2'
f_vec2 = HogFeatures(I);%Histogram of Gradient
f_vec4 =HocFeatures(I);%Histogram of Colors
f_vec=[I_std/127 f_vec2 f_vec4 ];%the feature vector

svmStruct=N.hist2Struct;

%learn mode is used for training first svm classifier
if learn_mode==false
    %first classifier should run fast and decide if the crop might be a
    %target
    group = svmclassify(svmStruct,f_vec);
    group_str = cell2mat(group);
    grade_svmStruct= svm_grade;
    if svm_limit>0
        if grade_svmStruct<svm_limit
            group_str='L';
        else
            group_str='N';
        end
    end
    switch (group_str)
        case 'L'
            %if the first classier decided it might be a target , we first
            %set the output for manual check , and then go for deeper
            %analyze
            output=2;%manual
                        
            %case 'hist2'
            %this is the acctual code that run on the competition
            
            [letter_bw_swt]=ExtractLetterInSWT(I);%use SWT algo to get letter out of the crop
            [pixel_labels] = MeanShiftABC(I); %run a mean shift on the crop to get several means
            [letter_bw_shift]= ExtractLetter(pixel_labels);%extract letter from the shift mean result
            corr = CorrelateBW(letter_bw_swt,letter_bw_shift);%calc correlation between the letters
            
            if plot_mode==true
                figure(5);cla;imagesc(pixel_labels);title('PIXEL LABEL');
                figure(6);cla;imagesc(letter_bw_swt);title('SWT');
                figure(7);cla;imagesc(letter_bw_shift);title('SHIFT MEAN');
            end
            
            %if there is good corrletion we proceed i.e. both
            %previous algo did found a letter inside the crop
            
            if  corr<BW_correlate_limit %~isinf(corr)
                           
                %run Nadav ocr to get connfident and letter params
                ocr_results_swt=myocr('ocr',uint8(letter_bw_swt)',yaw);
                ocr_conf_swt=str2double(ocr_results_swt(3));
                ocr_results_shift=myocr('ocr',uint8(letter_bw_shift)',yaw);
                ocr_conf_shift=str2double(ocr_results_shift(3));
              
                %if confident is high , it is a letter
                if ocr_conf_swt>min_ocr_result || ocr_conf_shift >min_ocr_result
                    
                    if ocr_conf_swt>ocr_conf_shift
                        letter_alphanumeric= ocr_results_swt(1);
                        letter_orientation= ocr_results_swt(2);
                        letter_bw=letter_bw_swt;
                    else
                        letter_alphanumeric= ocr_results_shift(1);
                        letter_orientation= ocr_results_shift(2);
                        letter_bw=letter_bw_shift;
                    end
                  
                    % letter_bw=letter_bw_shift;
                   
                    
                    %extract the sign by first deleting the letter
                    %from it
                    
                    [I_deleted,letter_color_RGB]=DeleteLetterFromImage(I,letter_bw);
                    [sign_bw,sign_color_RGB] = GetSignUsingLetter(I,I_deleted,letter_bw);
                    
                    %get colors of sign and letter
                    letter_color= (RGB2NameB(letter_color_RGB));
                    sign_color=(RGB2NameB(sign_color_RGB));
                  
                    imwrite(letter_bw,[output_path file_name_neto '_letter_bw' '.jpg'],'jpg');
                    imwrite(sign_bw,[output_path file_name_neto '_sign_bw' '.jpg'],'jpg');
                    
                    %get the sign using python code
                    [status, sign_shape] = system(['C:\Python27\python.exe C:\AUVSI\scripts\shapeDet.py ' output_path file_name_neto '_sign_bw' '.jpg']);
                 
                    output=1;%automatic
                    
                    if plot_mode==true
                        figure(9);cla;imagesc(I);title('ORG');
                        figure(10);cla;imagesc(I_deleted);title('DELETE LETTER');
                        figure(10);cla;imagesc(sign_bw);title('SIGN');
                    end
                end
                
            end
            
            
            
        case 'N'
            output=0;
    end
    output=[output grade_svmStruct];
    
    %write data for debug
    %csvwrite([output_path file_name_neto '.dat'],[output f_vec])
    
else
    output=f_vec;
end

end
