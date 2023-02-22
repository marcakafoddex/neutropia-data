layout (points) in;
layout (triangle_strip, max_vertices = 4) out;

#include "tools/color.glsl"

in VS_OUT {
	vec4 Uvs;
	mat3 TexMatrix;
	vec4 Color;
	vec2 Scale;
	mat4 WorldMatrix;
} vs_in[1];

uniform mat4 gVP;

out GS_OUT {
	vec2 Uv;
	vec4 Color;
} gs_out;

void main()
{
	mat4 wvp = gVP * vs_in[0].WorldMatrix;

	vec4 color = vs_in[0].Color;
	vec2 tlUV = vs_in[0].Uvs.xy;
	vec2 brUV = vs_in[0].Uvs.zw;
	
	vec2 halfScale = vs_in[0].Scale * 0.5;
	
	vec3 tr = gl_in[0].gl_Position.xyz + vec3( halfScale.x, -halfScale.y, 0.0);
	vec3 br = gl_in[0].gl_Position.xyz + vec3( halfScale.x,  halfScale.y, 0.0);
	vec3 bl = gl_in[0].gl_Position.xyz + vec3(-halfScale.x,  halfScale.y, 0.0);
	vec3 tl = gl_in[0].gl_Position.xyz + vec3(-halfScale.x, -halfScale.y, 0.0);

	gl_Position = wvp * vec4(tr, 1.0);
	gs_out.Uv = (vs_in[0].TexMatrix * vec3(brUV.x, tlUV.y, 1)).xy;
	gs_out.Color = color;
	EmitVertex();

	gl_Position = wvp * vec4(tl, 1.0);
	gs_out.Uv = (vs_in[0].TexMatrix * vec3(tlUV.x, tlUV.y, 1)).xy;
	gs_out.Color = color;
	EmitVertex();

	gl_Position = wvp * vec4(br, 1.0);
	gs_out.Uv = (vs_in[0].TexMatrix * vec3(brUV.x, brUV.y, 1)).xy;
	gs_out.Color = color;
	EmitVertex();

	gl_Position = wvp * vec4(bl, 1.0);
	gs_out.Uv = (vs_in[0].TexMatrix * vec3(tlUV.x, brUV.y, 1)).xy;
	gs_out.Color = color;
	EmitVertex();

	EndPrimitive();
}
