Shader "Custom/Grayscale"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _GrayFactor ("GrayFactor", Range(0,1)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 _Color;
            float _GrayFactor;

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                float gray = (col.r + col.g + col.b) / 3;
                //col.rgb = lerp(col.rgb,float3(gray, gray, gray),_GrayFactor);
                return col;
            }
            ENDCG
        }
    }
}
