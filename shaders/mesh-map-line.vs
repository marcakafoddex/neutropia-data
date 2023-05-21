#include "tools/color.glsl"

layout (location = 0) in vec3 Position;
layout (location = 5) in vec4 Color;
layout (location = 6/*+7+8+9*/) in mat4 WorldMatrix;

layout (location = 0) flat out vec3 startPos;
layout (location = 1) out vec3 vertPos;
layout (location = 2) out vec4 vColor;

uniform mat4 gVP;

void main()
{
	vec4 pos    = gVP * WorldMatrix * vec4(Position, 1.0);
    gl_Position = pos;
    vertPos     = pos.xyz / pos.w;
    startPos    = vertPos;
	vColor      = Color;
}
