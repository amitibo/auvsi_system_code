%Code subject: Simple input function to get some 2d  points
%Programmer: Aaron Wetzler, aaronwetzler@gmail.com
%Date:12/12/2009

function pnts= getPoints(n)

figure(1);title('Please enter 12 points');
hold on;
axis equal;
axis manual;

[xx,yy]=ginput(1);
plot(xx,yy,'.b');

for i=2:n
[x,y,button]=ginput(1);

plot(x,y,'.b');
xx=[xx;x];
yy=[yy;y];
end;
hold off;
axis normal;
xx=[xx; xx(1)]; 
yy=[yy; yy(1)];

line(xx,yy);
pnts=[xx yy];
