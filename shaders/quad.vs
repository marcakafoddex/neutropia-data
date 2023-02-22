#include "tools/color.glsl"

layout (location = 0) in vec4 Uvs;
layout (location = 1/*+2+3*/) in mat3 TexMatrix;
layout (location = 4) in vec4 Color;
layout (location = 5) in vec2 Scale;
layout (location = 6) in vec3 Center;
layout (location = 7/*+8+9+10*/) in mat4 WorldMatrix;

out VS_OUT {
	vec4 Uvs;
	mat3 TexMatrix;
	vec4 Color;
	vec2 Scale;
	mat4 WorldMatrix;
} vs_out;

void main()
{
	gl_Position	= vec4(Center, 1.0);
	vs_out.Scale = Scale;
	vs_out.Uvs = Uvs;
	vs_out.TexMatrix = TexMatrix;
	vs_out.Color = Color / 255.0;
	vs_out.Scale = Scale;
	vs_out.WorldMatrix = WorldMatrix;
}
