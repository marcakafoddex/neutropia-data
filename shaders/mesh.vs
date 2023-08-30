#include "tools/color.glsl"

// IN: fixed
layout (location = 0) in vec3 Position;
layout (location = 1) in vec2 Uv;
layout (location = 5) in vec4 Color;

// IN: instanced
layout (location = 2/*+3+4*/) in mat3 TexMatrix;
layout (location = 6/*+7+8+9*/) in mat4 WorldMatrix;

// IN: fixed custom
layout (location = 10) in vec2 LinePos;

// OUT
layout (location = 0) out vec2 vUv;
layout (location = 1) out vec4 vColor;
layout (location = 2) out vec2 vLinePos;

uniform mat4 gVP;

void main()
{
	gl_Position	= gVP * WorldMatrix * vec4(Position, 1.0);
	vUv = (TexMatrix * vec3(Uv, 1.0)).xy;
	vColor = Color / 255.0;		// comes in as byte values
	vLinePos = LinePos;
}
