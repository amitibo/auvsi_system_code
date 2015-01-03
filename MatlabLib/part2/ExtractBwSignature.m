function [signature,centroids] = ExtractBwSignature(bw)

[signature,centroids] = GetSortPerimeter(bw);
%[signature,centroids] = GetContourSortPerimeter(bw);

%normalize
max_s =max(signature(:,2));
min_s=min(signature(:,2));


signature(:,2)=(signature(:,2)-min_s)/(max_s);
signature(:,2)=signature(:,2)-mean(signature(:,2));
figure(18);plot(signature(:,1),signature(:,2));axis tight
