#include "tools/outline.glsl"

layout (location = 0) in vec2 vUv;
layout (location = 1) in vec4 vColor;

layout (location = 0) out vec4 FragColor;

uniform sampler2D gTexture;
uniform vec3 gBorderColor;
uniform vec3 gTopColor;
uniform vec3 gBottomColor;
uniform float gColorSplit;		// where on the y-axis is the text split
uniform int gHaveBorder;

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
	FragColor = renderOutlined(gTexture, vUv, vColor * vec4(textColor, 1.0), vec4(gBorderColor, 1.0), gHaveBorder);
}
