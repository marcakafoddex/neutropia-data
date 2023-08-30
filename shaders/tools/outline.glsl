vec4 renderOutlined(sampler2D sampler, vec2 uv, vec4 renderColor, vec4 borderColor, int haveBorder, int isFont, float alphaThreshold) {
	vec4 color = texture(sampler, uv) * renderColor;
	if (color.a > alphaThreshold) {
		if (isFont == 1)
			return renderColor;
		return color;
	} else {
		if (haveBorder == 0)
			return vec4(0.0, 0.0, 0.0, 0.0);
		else {
			vec2 texSize = textureSize(sampler, 0);
			vec2 nextPixelUvOffset = vec2(1,1) / texSize;
			if (false ||
				// 3 pixels to our right
				(texture(sampler, vec2(uv.x + nextPixelUvOffset.x, 	uv.y - nextPixelUvOffset.y	)) * renderColor).a > alphaThreshold ||
				(texture(sampler, vec2(uv.x + nextPixelUvOffset.x, 	uv.y						)) * renderColor).a > alphaThreshold ||
				(texture(sampler, vec2(uv.x + nextPixelUvOffset.x, 	uv.y + nextPixelUvOffset.y	)) * renderColor).a > alphaThreshold ||
				// 3 pixels to our left
				(texture(sampler, vec2(uv.x - nextPixelUvOffset.x, 	uv.y - nextPixelUvOffset.y	)) * renderColor).a > alphaThreshold ||
				(texture(sampler, vec2(uv.x - nextPixelUvOffset.x, 	uv.y						)) * renderColor).a > alphaThreshold ||
				(texture(sampler, vec2(uv.x - nextPixelUvOffset.x, 	uv.y + nextPixelUvOffset.y	)) * renderColor).a > alphaThreshold ||
				// 2 pixels above/below
				(texture(sampler, vec2(uv.x, 							uv.y + nextPixelUvOffset.y	)) * renderColor).a > alphaThreshold ||
				(texture(sampler, vec2(uv.x, 							uv.y - nextPixelUvOffset.y	)) * renderColor).a > alphaThreshold ||
				
				false)
				return borderColor;
			else
				return vec4(0.0, 0.0, 0.0, 0.0);
		}
	}
}
