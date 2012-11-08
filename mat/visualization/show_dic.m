clear;

rootdir = 'C:/Users/Administrator/Kinect/homp_release/';
% load([rootdir 'rgbd_dic_16x16_gray.mat']);
% figure(1);
% plot_dic(dic);
% title('Grayscale Dictionary');
% load([rootdir 'rgbd_dic_16x16_rgb.mat']);
% figure(2);
% plot_dic_color(dic);
% title('Color Dictionary');
% 
% load([rootdir 'rgbd_dic_16x16_depth.mat']);
% figure(3);
% plot_dic(dic);
% title('Depth Dictionary');
% load([rootdir 'rgbd_dic_16x16_normal.mat']);
% figure(4);
% plot_dic_color(dic);
% title('Surface Normal Dictionary');

% load([rootdir 'rgbd_dic_5x5_gray.mat']);
% figure(5);
% plot_dic(dic);
% title('Grayscale Dictionary');
% load([rootdir 'rgbd_dic_5x5_rgb.mat']);
% figure(6);
% plot_dic_color(dic);
% title('Color Dictionary');
% 
% load([rootdir 'rgbd_dic_5x5_depth.mat']);
% figure(7);
% plot_dic(dic);
% title('Depth Dictionary');
% load([rootdir 'rgbd_dic_5x5_normal.mat']);
% figure(8);
% plot_dic_color(dic);
% title('Surface Normal Dictionary');

load([rootdir 'rgbd_dic_16x16_depth.mat']);
figure(7);
plot_dic(dic);
title('Depth Dictionary');