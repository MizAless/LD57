Shader "Custom/ScrollingTexture"
{
    Properties
    {
        _FromTex ("From Texture", 2D) = "white" {}
        _ToTex ("To Texture", 2D) = "white" {}
        
        _Color ("Tint Color", Color) = (1, 1, 1, 1)
        
        _Speed ("Scroll Speed", Float) = 1.0
        _Tiling ("Tiling (XY)", Vector) = (1, 1, 0, 0)
        _LerpFactor ("Lerp Factor (0=From, 1=To)", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
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

            sampler2D _FromTex;
            sampler2D _ToTex;
            float4 _FromTex_ST;
            float4 _ToTex_ST;
            fixed4 _Color;
            float _Speed;
            float2 _Tiling;
            float _LerpFactor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _Tiling;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 scrollingUV = i.uv + float2(0, _Time.y * _Speed);
                scrollingUV = frac(scrollingUV);

                fixed4 fromColor = tex2D(_FromTex, scrollingUV);
                fixed4 toColor = tex2D(_ToTex, scrollingUV);
                
                fixed4 finalColor = lerp(fromColor, toColor, _LerpFactor) * _Color;
                
                return finalColor;
            }
            ENDCG
        }
    }
}