Shader "Unlit/Shockwave"
{
    Properties
    {
        _WaveDistance ("Wave Distance", Float) = 0.5
        _WaveWidth ("Wave Width", Float) = 0.3
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _LeadingColor ("Leading Color", Color) = (1,1,1,1)
        _TrailingColor ("Trailing Color", Color) = (1,1,1,1)
        _Persistance ("Persistance", Range(0.0, 2.0)) = 1.5
        _Scale ("Scale", Float) = 5
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            float _WaveDistance;
            float _WaveWidth;
            float4 _BaseColor;
            float4 _LeadingColor;
            float4 _TrailingColor;
            float _Persistance;
            float _Scale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.vertex+0.5;
                return o;
            }

            float random(float3 position){
                return frac(sin(dot(position, float3(12.9898,78.233,29.1653)))*43758.5453123);
            }

            float noise(float3 position){
                float3 i = floor(position);
                float3 f = frac(position);
                f = f*f*(3.0-2.0*f);
                return lerp(lerp(lerp(random(i), random(i+float3(1,0,0)),f.x),
                                lerp(random(i+float3(0,1,0)), random(i+float3(1,1,0)),f.x),f.y),
                            lerp(lerp(random(i+float3(0,0,1)), random(i+float3(1,0,1)),f.x),
                                lerp(random(i+float3(0,1,1)), random(i+float3(1,1,1)),f.x),f.y),f.z);
            }

            float fbm(float3 position){
                float total = 0.0;
                float persistence = _Persistance;
                float lacunarity = 2.;
                float3 shift = float3(100, 100, 100);
                for(int i = 0; i < 4; i++){
                    total += noise(position)*persistence;
                    position = position*lacunarity + shift;
                    persistence *= 0.5;
                }
                return total;
            }

            float4 liquid(float4 color, v2f i){
                i.color *= _Scale;
                float3 offset = float3(_SinTime.w*10,_CosTime.w*10,_SinTime.w*10);
                float n1 = fbm(i.color.xyz+offset);
                float n2 = fbm(i.color.xyz-offset);
                float f = fbm(i.color.xyz + fbm(i.color.xyz + fbm(i.color.xyz+ offset)));
                color = color*f*n1*n2;
                return color;
            }

            bool inRadius(float2 uv, float2 center, float radius)
            {
                if(radius < 0) return false;
                float2 diff = uv - center;
                return dot(diff, diff) < radius * radius;
            }

            bool wave(float2 uv, float2 center, float innerRadius, float outerRadius)
            {
                return inRadius(uv, center, outerRadius)
                    && !inRadius(uv, center, innerRadius);
            }

            float4 fade(float4 color, float distance)
            {
                color.a = max(0,lerp(1, 0, distance / 0.5));
                return color;
            }

            float4 setColor(float4 color, float2 uv, float innerRadius, float outerRadius)
            {
                float d = distance(uv, float2(0.5, 0.5)) - _WaveWidth/2;

                float3 c1 = lerp(_BaseColor.rgb, _LeadingColor.rgb, d / _WaveWidth/2);
                float3 c2 = lerp(_BaseColor.rgb, _TrailingColor.rgb, d / _WaveWidth/2);
                float3 c = lerp(c1, c2, d / _WaveWidth/2);

                return float4(c,1);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                _WaveDistance = _WaveDistance-_WaveWidth/2;
                float innerRadius = max(0, _WaveDistance - _WaveWidth);
                float outerRadius = _WaveDistance + _WaveWidth;
                bool inWave =  wave(i.uv, float2(0.5, 0.5), innerRadius, outerRadius);

    
                if(!inWave) discard;

                // sample the texture
                float4 col = setColor(col, i.uv, innerRadius, outerRadius);
                col = liquid(col, i);
                col = fade(col, distance(i.uv, float2(0.5, 0.5)));

                return col;
            }
            ENDCG
        }
    }
}
