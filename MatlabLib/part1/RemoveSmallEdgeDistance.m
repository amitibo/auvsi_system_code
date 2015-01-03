
function [I_edge] = RemoveSmallEdgeDistance(I_edge,limit)


properties{1}='Centroid';
properties{2}='Area';
properties{3}='PixelIdxList';

STATS = regionprops(I_edge,properties);
num_of_components=length(STATS);
[x y] = size(I_edge);
x_center=(x+1)/2;y_center=(y+1)/2;
max_dist=norm([x_center y_center]);
if num_of_components>0
    
    for i=1:(num_of_components)
        center = 1-norm(STATS(i).Centroid-[x_center y_center])/max_dist;
        grade =  (center*STATS(i).Area);
        if grade<limit
            I_edge(STATS(i).PixelIdxList) = 0;
        end
    end
end
end