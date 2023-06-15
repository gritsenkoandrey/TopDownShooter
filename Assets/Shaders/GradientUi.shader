Shader "Unlit/UI Lerp Gradient Angle Direction"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _GradientColor ("Gradient Color", Color) = (0,0,0,0)
        _Percent ("Percent", Range(0, 2)) = 0.5
        _AngleDirection ("Angle Direction (Degrees)", Range(0.0, 360.0)) = 0.0
    }
 
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
 
            float4 _Color;
            float4 _GradientColor;
            float _AngleDirection;
            float _Percent;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = _Color;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                float angle = _AngleDirection * 0.0174533; // convert degrees to radians
                float2 dir = float2(cos(angle), sin(angle));
                float2 normalizedUV = i.uv - _Percent; // move origin to center of texture
                float dotProduct = dot(normalizedUV, dir);
                float t = (dotProduct + 0.5) * 2.0; // shift range from [-1, 1] to [0, 1]
                fixed4 c = lerp(_GradientColor, _Color, t);
                return c;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Texture"
}