
Shader "Unlit/HeatHazeShader"
{
    Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _AlphaTex ("AlphaTexture", 2D) = "white" {}
        _NoiseTex("NoiseTex", 2D) = "white" {}
        _Distortion("Distortion", Range(0,0.1)) = 0.005
        _Speed("Speed", Range(0,500)) = 50 
        _amount ("_amount", Range(0.01, 1.0)) = 0.05
        _spread ("_spread", Range(0.01, 1.0)) = 0.05
        _width ("_width", Range(0.01, 1.0)) = 0.05
        _alpha ("_alpha", Range(0.01, 1.0)) = 0.05
	}
	SubShader
	{
		Pass
		{
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
            sampler2D _NoiseTex;
            sampler2D _CameraSortingLayerTexture;
            sampler2D _AlphaTex;
            float4 _MainTex_ST;
            float _amount;
            float _spread;
            float _width;
            float _alpha;
            float _Distortion;
            float _Speed;

			struct vertIn
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
                float4 screenPos: TEXCOORD3;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
                float4 screenPos: TEXCOORD3;
			};        
            
            // Implementation of the vertex shader
			vertOut vert(vertIn v)
			{

				vertOut o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//o.uv = v.uv;
		        o.screenPos = ComputeScreenPos(o.vertex);
				return o;
			}
			
			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
                float strength = tex2Dlod(_AlphaTex, float4 (v.uv, 0, 0)).a;
				float noise = tex2Dlod(_NoiseTex, float4 (v.uv, 0, 0)).a;

                v.screenPos.x -= cos(_Time * _Speed * noise) * _Distortion * strength;
                v.screenPos.y -= sin(_Time * _Speed * noise) * _Distortion * strength;

                float4 color = tex2Dlod(_CameraSortingLayerTexture, float4 (v.screenPos.xy, 0, 0));
                
				return float4(color);
			}
			ENDCG
		}
	}

    
}