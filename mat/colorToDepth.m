function depthImage = colorToDepth(colorImage)
blue = colorImage(:,:,3);
green = colorImage(:,:,2);
BLUE = uint16(blue);
GREEN = uint16(green);
depthImage = bitsll(BLUE, 8)+GREEN;
end