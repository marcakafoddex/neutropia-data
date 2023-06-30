#include "tools/outline.glsl"

layout (location = 0) in vec2 vUv;
layout (location = 1) in vec4 vColor;

layout (location = 0) out vec4 FragColor;

uniform sampler2D gTexture;
uniform vec4 gBorderColor;

void main()
{
	FragColor = renderOutlined(gTexture, vUv, vColor, gBorderColor, 1);
}
