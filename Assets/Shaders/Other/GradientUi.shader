Shader "Unlit/Gradient"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _GradientColor ("Gradient Color", Color) = (0,0,0,0)
        _Percent ("Percent", Range(0, 2)) = 0.5
        _AngleDirection ("Angle Direction (Degrees)", Range(0.0, 360.0)) = 0.0

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }
 
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
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
 
            half4 _Color;
            half4 _GradientColor;
            half _AngleDirection;
            half _Percent;
 
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
                half angle = _AngleDirection * 0.0174533;
                half2 direction = float2(cos(angle), sin(angle));
                half2 normalizedUV = i.uv - _Percent;
                half dotProduct = dot(normalizedUV, direction);
                half time = (dotProduct + 0.5) * 2.0;
                fixed4 c = lerp(_GradientColor, _Color, time);
                return c;
            }
            
            ENDCG
        }
    }
    FallBack "Unlit/Texture"
}