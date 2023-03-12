layout (location = 0) in vec2 vUv;
layout (location = 1) in vec4 vColor;

layout (location = 0) out vec4 FragColor;

uniform sampler2D gTexture;
uniform vec3 gBorderColor;
uniform vec2 gPixelScale;		// size in screen pixels(!) of the outline on X and Y axes
uniform vec3 gTopColor;
uniform vec3 gBottomColor;
uniform float gColorSplit;		// where on the y-axis is the text split

const float kAlphaThreshold = 0.5;

void main()
{
	vec3 textColor;
	if (gColorSplit >= 1.0)
		textColor = gTopColor;
	else if (gColorSplit <= 0.0)
		textColor = gBottomColor;
	else if (vUv.y <= gColorSplit)
		textColor = gTopColor;
	else
		textColor = gBottomColor;		
	vec4 color = texture(gTexture, vUv) * vColor * vec4(textColor, 1.0);
	
	if (color.a > kAlphaThreshold)
		FragColor = color;
	else {
		vec2 texSize = textureSize(gTexture, 0);
		vec2 nextPixelUvOffset = gPixelScale / texSize;
		if (false ||
			// 3 pixels to our right
			(texture(gTexture, vec2(vUv.x + nextPixelUvOffset.x, 	vUv.y - nextPixelUvOffset.y	)) * vColor).a > kAlphaThreshold ||
			(texture(gTexture, vec2(vUv.x + nextPixelUvOffset.x, 	vUv.y						)) * vColor).a > kAlphaThreshold ||
			(texture(gTexture, vec2(vUv.x + nextPixelUvOffset.x, 	vUv.y + nextPixelUvOffset.y	)) * vColor).a > kAlphaThreshold ||
			// 3 pixels to our left
			(texture(gTexture, vec2(vUv.x - nextPixelUvOffset.x, 	vUv.y - nextPixelUvOffset.y	)) * vColor).a > kAlphaThreshold ||
			(texture(gTexture, vec2(vUv.x - nextPixelUvOffset.x, 	vUv.y						)) * vColor).a > kAlphaThreshold ||
			(texture(gTexture, vec2(vUv.x - nextPixelUvOffset.x, 	vUv.y + nextPixelUvOffset.y	)) * vColor).a > kAlphaThreshold ||
			// 2 pixels above/below
			(texture(gTexture, vec2(vUv.x, 							vUv.y + nextPixelUvOffset.y	)) * vColor).a > kAlphaThreshold ||
			(texture(gTexture, vec2(vUv.x, 							vUv.y - nextPixelUvOffset.y	)) * vColor).a > kAlphaThreshold ||
			
			false)
			FragColor = vec4(gBorderColor, 1);
		else
			FragColor = vec4(0, 0, 0, 0);
	}
	
}
