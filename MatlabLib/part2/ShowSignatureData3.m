clear all
close all


load 'C:\AUVSI\svm-files\SHAPES_DB.mat'

for i=1:length(SHAPES_DB.crops)

    crop = SHAPES_DB.crops{i};
    figure(1);cla;imagesc(crop);title( SHAPES_DB.bbs_type{i});

    signature = SHAPES_DB.signature{i};
  
    
    figure(2);plot(signature(:,1),signature(:,2));axis tight
end

