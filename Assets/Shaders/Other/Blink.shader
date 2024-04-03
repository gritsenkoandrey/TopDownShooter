Shader "Custom/Blink"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlinkColorOne ("Color One", Color) = (1, 1, 1, 1)
        _BlinkColorTwo ("Color Two", Color) = (1, 1, 1, 1)
        _BlinkSpeed ("Speed", Range (0, 20)) = 1
        _Size ("Size", Range(0, 20)) = 1
        _MultiplyOne ("Multiply One", Range(0, 5)) = 1
        _MultiplyTwo ("Multiply Two", Range(0, 5)) = 1
        _BlinkActivate ("Activate", Range (0, 1)) = 0
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
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _BlinkColorOne;
            float4 _BlinkColorTwo;
            float _BlinkSpeed;
            float _BlinkActivate;
            float _Size;
            float _BlinkTimer;
            float _MultiplyOne;
            float _MultiplyTwo;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                if (_BlinkActivate == 1)
                {
                    _BlinkTimer = sin(_Time.y * _BlinkSpeed) * _MultiplyOne + _MultiplyTwo;
                }

                float remap = sin(_Time.y * _BlinkSpeed + i.uv.y * _Size);
                fixed4 remapColor = lerp(_BlinkColorOne, _BlinkColorTwo, remap);
                fixed4 tex = tex2D(_MainTex, i.uv);
                fixed4 color = lerp(tex, remapColor, _BlinkTimer);

                return color;
            }
            
            ENDCG
        }
    }
 
    FallBack "Diffuse"
}