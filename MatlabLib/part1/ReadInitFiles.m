
%this function use to init the data and is called only once
function [coeff_data,mat_data] = ReadInitFiles()
svm_path='C:\AUVSI\svm-files\';
coeff_data= importdata([svm_path 'SvmInit.txt'], ' ', 1);


lnStruct=[];

load([svm_path 'hist2Struct.mat']);
load([svm_path 'lnStruct.mat']);
load([svm_path 'newColorNames.mat']);
load([svm_path 'extraData.mat']);


mat_data.hist2Struct=hist2Struct;
mat_data.lnStruct=lnStruct;
mat_data.newColorNames=newColorNames;
mat_data.extraData=extraData;

num_rows=coeff_data.data(1,2);
num_cols=coeff_data.data(1,3);
pow=coeff_data.data(1,8);

[D_loc,Y_loc,X_loc] = LocMatrix(num_rows,num_cols,pow);
[D_loc_N,Y_loc_N,X_loc_N] = NormalizedLocMatrix(num_rows,num_cols,pow);

coeff_data.D_loc_N=D_loc_N;
coeff_data.Y_loc_N=Y_loc_N;
coeff_data.X_loc_N=X_loc_N;

coeff_data.D_loc=D_loc;
coeff_data.Y_loc=Y_loc;
coeff_data.X_loc=X_loc;
end
