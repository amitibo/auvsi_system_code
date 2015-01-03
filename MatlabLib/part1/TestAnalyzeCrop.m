if ~exist('svmCoeff')
[svmCoeff,svmStruct] = ReadInitFiles();
end

clc
global plot_mode
plot_mode=true;
target_file_name = 'C:\Users\itayguy\Downloads\crop_92_2.jpg';
target_file_name = 'C:\Users\itayguy\Downloads\crop_20_0.jpg';
% crop_path = 'E:\crops\crops\TargetShomrat\';
% crop_path = 'C:\Users\itayguy\Documents\Technion\UAS\data\false targets\';
% crop_path = 'C:\Users\itayguy\Documents\Technion\UAS\data\targets\';
% crop_path = 'C:\Users\itayguy\Documents\Technion\UAS\data\competition\';
crop_path = 'F:\Users\itayguy\Documents\Technion\UAS\data\competition\recognized\';

%crop_path = 'E:\competition\hand_crops\';

%crop_path = 'E:\bad tag\letter\';
%crop_path = 'E:\no orientation\';
%crop_path = 'E:\bad tag\check\';
%crop_path = 'C:\Users\itayguy\Documents\Technion\UAS\data\ocr\';
%crop_path = 'C:\Users\itayguy\Documents\Technion\UAS\data\real\';

crop_dir=dir([crop_path '*.jpg']);
num_y_files=length(crop_dir);
for i=1:num_y_files
       i=1
%     num_y_files-i

    file_name = crop_dir(i).name
    target_file_name=[crop_path file_name];
    %target_file_name='C:\Users\itayguy\Documents\Technion\UAS\data\targets\diamon_A.jpg';
    I=imread(target_file_name);
%      ocr_results_swt=myocr('ocr',im2bw(I)',177)
    figure(1);cla;imagesc(I);title('ORG');
    [output,letter_color,sign_color,letter_alphanumeric,letter_orientation,sign_shape]=AnalyzeCrop...
        (target_file_name,svmCoeff,svmStruct,0)
end
