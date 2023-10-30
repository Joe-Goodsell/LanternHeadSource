// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/HeatHaze2D"
{
    Properties
    {
        _MainTex("Texture", 2D)                  = "white" {}
        _NoiseTex("NoiseTex",2D)                 = "white" {}
        _Distortion("Distortion", Range(0, 0.1)) = 0.03
        _Speed("Speed", Range(0, 50))            = 10 
    }

    SubShader
    {
        Tags 
        { 
            "Queue"          = "Transparent" 
            "RenderType"     = "Transparent" 
            //"RenderPipeline" = "UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        //ZWrite off
        Cull off

        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv     : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CameraSortingLayerTexture;
            sampler2D _NoiseTex;
            float _Distortion;
            float _Speed;

            v2f vert (appdata_t v)
            {
                v2f o;
                //o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half2 uv = i.uv; 
                half4 noiseTex = tex2D(_NoiseTex, uv);
                i.screenPos.x += cos(_Time * _Speed) * noiseTex.rgb * _Distortion;
                i.screenPos.y += sin(_Time * _Speed)  * noiseTex.rgb * _Distortion;
                half4 col = tex2D(_CameraSortingLayerTexture, i.screenPos);
                return float4(col.xyz, 0.5);
            }
            ENDCG
        }
    }
}