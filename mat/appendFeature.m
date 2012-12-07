function appendFeature(rgbdfea_file, newfea)
load(rgbdfea_file);
newclabel = (max(rgbdclabel) + 1)*ones(1, size(newfea, 2));
rgbdclabel = [rgbdclabel newclabel];
rgbdfea = [rgbdfea newfea];
save -v6 feature_new rgbdfea rgbdclabel rgbdilabel rgbdvlabel;