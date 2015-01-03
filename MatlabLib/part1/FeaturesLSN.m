
function f_vec = FeaturesLSN(I,STATS)

global M;
num_rows=M.data(1,2);
num_cols=M.data(1,3);
area_limit=M.data(1,10);
swt_max_width=M.data(1,11);
swt_min_averege_limit=M.data(1,15);


if nargin<2
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
    
    STATS = regionprops(I,properties);
end

if isempty(STATS)
    f_vec=zeros(1,12);
    return;
end

if STATS.Area<area_limit
    %     averege_swt=0;
    %     num_cc_swt=0;
    %     swt_area=0;
    
    f_vec=zeros(1,12);
    return;
else
    
    [swt,swtcc] = SwtLight( I,bwperim(I, 8), 1,swt_max_width-2);
    %     [I_edge,I_gauss] = FastEdge(I);
    %     [swt,swtcc] = SwtLight(I_gauss,I_edge, 1,swt_max_width-2);
    %     figure(7);cla;imagesc(I_gauss)
    %     figure(8);cla;imagesc(I_edge)
    %     figure(9);cla;imagesc(swt)
    %     figure(11);cla;imagesc(swtcc)
    num_cc_swt=max(max(swtcc));
    if num_cc_swt==0
%         swt_area =0;
%         averege_swt =0;
        f_vec=zeros(1,12);
        return;
    else
        swt_area = sum(sum(swt>0));
        averege_swt = sum(sum(uint8(swt)))/swt_area;
        if averege_swt<swt_min_averege_limit
%             num_cc_swt=0;
%             swt_area =0;
%             averege_swt =0;
           f_vec=zeros(1,12);
           return;
        end
    end
    %averege_swt=averege_swt/swt_max_width;
end

x_center=(num_cols+1)/2;y_center=(num_rows+1)/2;
center = norm(STATS.Centroid-[x_center y_center]);

%  area = STATS.Area/num_cols*num_rows;
%  major_axis_length=STATS.MajorAxisLength/num_cols;
%  minor_axis_length=STATS.MinorAxisLength/num_rows;

f_vec = [averege_swt num_cc_swt swt_area center STATS.Area STATS.MajorAxisLength STATS.MinorAxisLength...
    STATS.Eccentricity STATS.EulerNumber STATS.Solidity STATS.Extent STATS.ConvexArea];
end