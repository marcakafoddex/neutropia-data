/* Converts color from UINT to vec4 */
vec4 convertColor(highp uint rgba) {
	float a = float(rgba >> 24u) / 255.0;
	float b = float((rgba >> 16u) & 0xffu) / 255.0;
	float g = float((rgba >> 8u) & 0xffu) / 255.0;
	float r = float(rgba & 0xffu) / 255.0;
	return vec4(r, g, b, a);
}

/* Sets fade factor in UINT representation of color.
 * The alpha value is clamped inside 0-1 range,
 * and converted to proper 0-255 range if maxAlpha is 1.0
 * If it is e.g. 0.75, then the range becomes 0-191
 */
uint fadeColor(highp uint rgba, float a) {
	uint alpha = uint(255 * clamp(a, 0, 1));
	return (rgba & 0xffffffu) | (alpha << 24u);
}
