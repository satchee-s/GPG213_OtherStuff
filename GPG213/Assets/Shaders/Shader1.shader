Shader "Custom/Shader1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Texture2 ("Texture", 2D) = "white" {}
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
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _Texture2;
            float4 _Texture2_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv1 = TRANSFORM_TEX(v.uv1, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv2, _Texture2);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //float2 movingUV1 = i.uv1;
                float2 movingUV2 = i.uv2;
                movingUV2.x += _SinTime.y;
                movingUV2.y += _CosTime.y;
                fixed4 col1 = tex2D(_MainTex, i.uv1);
                fixed4 col2 = tex2D(_Texture2, movingUV2);
                return col1 * col2;
            }
            ENDCG
        }
    }
}
