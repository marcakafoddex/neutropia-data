layout (location = 0) flat in vec3 startPos;
layout (location = 1) in vec3 vertPos;

layout (location = 0) out vec4 FragColor;

uniform vec4 gLineColor;

uniform vec2  gResolution;
uniform float gDashSize;
uniform float gGapSize;

void main()
{
    vec2  dir  = (vertPos.xy - startPos.xy) * gResolution/2.0;
    float dist = length(dir);

    if (fract(dist / (gDashSize + gGapSize)) > gDashSize/(gDashSize + gGapSize))
        discard;

    FragColor = gLineColor;
}
