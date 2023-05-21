const float kAlphaThreshold = 0.7;

vec4 renderOutlined(sampler2D sampler, vec2 uv, vec4 renderColor, vec3 borderColor, int haveBorder) {
	vec4 color = texture(sampler, uv) * renderColor;
	if (color.a > kAlphaThreshold)
		return vec4(color);
	else {
		if (haveBorder == 0)
			return vec4(0.0, 0.0, 0.0, 0.0);
		else {
			vec2 texSize = textureSize(sampler, 0);
			vec2 nextPixelUvOffset = vec2(1,1) / texSize;
			if (false ||
				// 3 pixels to our right
				(texture(sampler, vec2(uv.x + nextPixelUvOffset.x, 	uv.y - nextPixelUvOffset.y	)) * renderColor).a > kAlphaThreshold ||
				(texture(sampler, vec2(uv.x + nextPixelUvOffset.x, 	uv.y						)) * renderColor).a > kAlphaThreshold ||
				(texture(sampler, vec2(uv.x + nextPixelUvOffset.x, 	uv.y + nextPixelUvOffset.y	)) * renderColor).a > kAlphaThreshold ||
				// 3 pixels to our left
				(texture(sampler, vec2(uv.x - nextPixelUvOffset.x, 	uv.y - nextPixelUvOffset.y	)) * renderColor).a > kAlphaThreshold ||
				(texture(sampler, vec2(uv.x - nextPixelUvOffset.x, 	uv.y						)) * renderColor).a > kAlphaThreshold ||
				(texture(sampler, vec2(uv.x - nextPixelUvOffset.x, 	uv.y + nextPixelUvOffset.y	)) * renderColor).a > kAlphaThreshold ||
				// 2 pixels above/below
				(texture(sampler, vec2(uv.x, 							uv.y + nextPixelUvOffset.y	)) * renderColor).a > kAlphaThreshold ||
				(texture(sampler, vec2(uv.x, 							uv.y - nextPixelUvOffset.y	)) * renderColor).a > kAlphaThreshold ||
				
				false)
				return vec4(borderColor, 1.0);
			else
				return vec4(0.0, 0.0, 0.0, 0.0);
		}
	}
}
