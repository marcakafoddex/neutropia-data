layout (location = 0) in vec2 vUv;
layout (location = 1) in vec4 vColor;

layout (location = 0) out vec4 FragColor;

uniform sampler2D gTexture;
uniform vec3 gBorderColor;
uniform vec2 gPixelScale;		// size in screen pixels(!) of the outline on X and Y axes

const float kAlphaThreshold = 0.5;

void main()
{
	vec4 color = texture(gTexture, vUv) * vColor;
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
