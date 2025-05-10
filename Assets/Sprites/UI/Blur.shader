Shader "Custom/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0, 10)) = 2.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            float _BlurSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 sum = fixed4(0,0,0,0);
                float2 offsets[9] = {
                    float2(-1,-1), float2(0,-1), float2(1,-1),
                    float2(-1,0),  float2(0,0),  float2(1,0),
                    float2(-1,1),  float2(0,1),  float2(1,1)
                };

                for (int j = 0; j < 9; j++)
                {
                    sum += tex2D(_MainTex, i.uv + offsets[j] * _BlurSize * 0.005);
                }

                return sum / 9.0;
            }
            ENDCG
        }
    }
}