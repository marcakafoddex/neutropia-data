in GS_OUT {
	vec2 Uv;
	vec4 Color;
} fs_in;

uniform sampler2D gTexture;

out vec4 FragColor;

void main()
{
	FragColor = texture(gTexture, fs_in.Uv) * fs_in.Color;
}
