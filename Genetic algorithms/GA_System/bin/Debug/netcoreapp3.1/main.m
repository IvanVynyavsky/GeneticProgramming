clc;
clear;
fileID = fopen('IterationBestResult.txt','r');
%fileID = fopen('ProgramBestResult.txt','r');
formatSpec = '%f';

y = fscanf(fileID,formatSpec);
for ii = 1:length(y)
  x(ii) = ii; 
endfor


hold on;
plot(x,y)

