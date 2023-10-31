Shader "Unlit/Waterfall_Foam"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FoamThreshold ("Foam Threshold", Range(0,1)) = 0.2
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

            sampler2D _CameraDepthTexture;
            float _FoamThreshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Mist and Foam
                float depth = tex2D(_CameraDepthTexture, i.uv).r;
                float foamAmount = saturate((depth - _FoamThreshold) / 0.1);
                col.rgb += foamAmount * float3(1, 1, 1);  // Just adding the foam effect to the RGB
                
                return col;
            }
            ENDCG
        }
    }
}