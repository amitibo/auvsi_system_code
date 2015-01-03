%this function uses Stroke Width Transform to find a letter in an Image
%it uses swt twich , first light on dark and then dark on light , then it
%uses svm to find somthing that looks like a letter,it uses SwtLight which
%uses external c code
function  [letter_bw]=ExtractLetterInSWT(I)

global M
area_limit=M.data(1,10);
maxWidth=M.data(1,11);

letter_bw=[];

[I_edge,I_gray] = CombineEdge(I);

%figure(16);cla;imagesc(I_edge)
limit=12;
%I_edge=RemoveSmallEdge(I_edge,limit);
I_edge = RemoveSmallEdgeDistance(I_edge,limit);%removes small edges based on distance from center
%figure(17);cla;imagesc(I_edge)

[swtIn1 swtccIn1] = SwtLight( I_gray,I_edge, 1, maxWidth );
temp_bw = bwareaopen(logical(swtccIn1), area_limit, 8);
[letter_bw1,grade1]= ExtractLetter(temp_bw);

% figure(3);cla;imagesc(swtccIn1);
% figure(4);cla;imagesc(temp_bw);
% figure(5);cla;imagesc(I_gray);
% figure(6);cla;imagesc(I_edge)


[swtIn0 swtccIn0] = SwtLight( I_gray,I_edge, 0, maxWidth );
temp_bw = bwareaopen(logical(swtccIn0), area_limit, 8);%makes it smother
[letter_bw0,grade0]= ExtractLetter(temp_bw);


% figure(9);cla;imagesc(I_rangefilt_edge)
% figure(10);cla;imagesc(swtIn0)
% figure(11);cla;imagesc(swtccIn0)



if grade1>grade0
   % if(grade1>0)
        letter_bw=letter_bw1;
   % end
else
   % if(grade0>0)
        letter_bw=letter_bw0;
   % end
    
end


% figure(12);cla;imagesc(letter_bw)

end