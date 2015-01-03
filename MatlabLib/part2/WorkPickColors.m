function WorkPickColors()

global N
global M

if ~exist('svmCoeff')
    [M,N] = ReadInitFiles();
end


clc

global plot_mode
plot_mode=true;

crop_path = 'C:\Users\itayguy\Documents\Technion\UAS\data\test colotrs\';
svm_path='C:\AUVSI\svm-files\';

crop_dir=dir([crop_path '*.jpg']);
num_y_files=length(crop_dir);
for i=1:num_y_files
    %        i=3 %=2
    %     num_y_files-i
    
    load([svm_path 'newColorNames.mat']);
    load([svm_path 'extraData.mat']);
    
    
    file_name = crop_dir(i).name
    target_file_name=[crop_path file_name];

    I=imread(target_file_name);

    figure(1);cla;imagesc(I);title('ORG');

    button=1;
    while button==1
        clc
        [x,y,button] = ginput(1);
        if button ==3
            continue;
        end
        if button ==2
            return;
        end
        i = round(y);j=round(x);
        roi_color = I(i-1:i+1,j-1:j+1,:);
        roi_color = mean(mean(roi_color));
        pixel_RGB = round(squeeze(roi_color));%= squeeze(I(i,j,:));
        
        RGB2NameB(pixel_RGB)
        newColorNames=input('new name? :','s');
        if ~isempty(newColorNames)
            extraData.colorRGB = [extraData.colorRGB; pixel_RGB';];
            extraData.colorNames = [extraData.colorNames newColorNames ];
            save 'C:\AUVSI\svm-files\extraData.mat' extraData
        end
        N.extraData=extraData;
     
        
    end
    
end

end