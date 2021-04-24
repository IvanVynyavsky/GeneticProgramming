clc;
clear;

f = @(x) sin(x) + cos(3)*2;

x = linspace(-pi,pi,50)

y = f(x)

for ii = 1:1:51
  y(ii) = sin(2) + cos(3)*2;
end

fileID = fopen('data.txt','w');
fprintf(fileID,'%d ',x);
fprintf(fileID,'\n');
fprintf(fileID,'%d ',y);
fclose(fileID);

plot(x,y)