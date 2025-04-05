Shader "Custom/ScrollingTexture"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1, 1, 1, 1)
        _Speed ("Scroll Speed", Float) = 1.0
        _Tiling ("Tiling (XY)", Vector) = (1, 1, 0, 0)
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _Speed;
            float2 _Tiling;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex) * _Tiling;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 scrollingUV = i.uv + float2(0, _Time.y * _Speed);
                scrollingUV = frac(scrollingUV);
                
                fixed4 texColor = tex2D(_MainTex, scrollingUV);
                fixed4 finalColor = texColor * _Color;
                
                return finalColor;
            }
            ENDCG
        }
    }
}