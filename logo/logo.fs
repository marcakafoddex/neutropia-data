#version 330

in vec2 TexCoord0;

uniform vec4 gColor;
uniform sampler2D gTexture;
uniform sampler2D gNoiseTexture;
uniform float gGrayness;
uniform float gNoise;
uniform float gSharpness;

out vec4 FragColor;

const vec2 poissonDisk[16] = vec2[](
	vec2(-0.613392, 0.617481),
	vec2(0.170019, -0.040254),
	vec2(-0.299417, 0.791925),
	vec2(0.645680, 0.493210),
	vec2(-0.651784, 0.717887),
	vec2(0.421003, 0.027070),
	vec2(-0.817194, -0.271096),
	vec2(-0.705374, -0.668203),
	vec2(0.977050, -0.108615),
	vec2(0.063326, 0.142369),
	vec2(0.203528, 0.214331),
	vec2(-0.667531, 0.326090),
	vec2(-0.098422, -0.295755),
	vec2(-0.885922, 0.215369),
	vec2(0.566637, 0.605213),
	vec2(0.039766, -0.396100)
);

const float kPixelation = 192.0;

void main()
{
	vec2 uv = TexCoord0;
	uv *= kPixelation;
	uv = round(uv);
	uv /= kPixelation;

	vec4 color = vec4(0,0,0,0);
	for (int i = 0; i < 16; ++i)
		color += texture(gTexture, uv + poissonDisk[i] * gSharpness);
	color /= 16.0;

	float noise = texture(gNoiseTexture, uv * 5.0).r;
	vec4 noiseColor = vec4(noise, noise, noise, color.a);
	color = mix(color, noiseColor, gNoise);

	float graytone = dot(color.rgb, vec3(0.333, 0.333, 0.334));
	vec4 grayColor = vec4(graytone, graytone, graytone, color.a);
	color = mix(color, grayColor, gGrayness);

	FragColor = color * gColor;
}
