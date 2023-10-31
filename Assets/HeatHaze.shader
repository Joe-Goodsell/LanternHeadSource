
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

			
            inline float unity_noise_randomValue (float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233)))*43758.5453);
            }

            inline float unity_noise_interpolate (float a, float b, float t)
            {
                return (1.0-t)*a + (t*b);
            }

            inline float unity_valueNoise (float2 uv)
            {
                float2 i = floor(uv);
                float2 f = frac(uv);
                f = f * f * (3.0 - 2.0 * f);

                uv = abs(frac(uv) - 0.5);
                float2 c0 = i + float2(0.0, 0.0);
                float2 c1 = i + float2(1.0, 0.0);
                float2 c2 = i + float2(0.0, 1.0);
                float2 c3 = i + float2(1.0, 1.0);
                float r0 = unity_noise_randomValue(c0);
                float r1 = unity_noise_randomValue(c1);
                float r2 = unity_noise_randomValue(c2);
                float r3 = unity_noise_randomValue(c3);

                float bottomOfGrid = unity_noise_interpolate(r0, r1, f.x);
                float topOfGrid = unity_noise_interpolate(r2, r3, f.x);
                float t = unity_noise_interpolate(bottomOfGrid, topOfGrid, f.y);
                return t;
            }

            float Unity_SimpleNoise_float(float2 UV, float Scale)
            {
                float t = 0.0;

                float freq = pow(2.0, float(0));
                float amp = pow(0.5, float(3-0));
                t += unity_valueNoise(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

                freq = pow(2.0, float(1));
                amp = pow(0.5, float(3-1));
                t += unity_valueNoise(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

                freq = pow(2.0, float(2));
                amp = pow(0.5, float(3-2));
                t += unity_valueNoise(float2(UV.x*Scale/freq, UV.y*Scale/freq))*amp;

                float Out = t;
                return Out;
            }            
            
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

                v.screenPos.x -= cos(_Time * _Speed * noise) * _Distortion * strength; // increase the second argument for more distorted effect
                v.screenPos.y -= sin(_Time * _Speed * noise) * _Distortion * strength;

                float4 color = tex2Dlod(_CameraSortingLayerTexture, float4 (v.screenPos.xy, 0, 0));
                
				return float4(color);
			}
			ENDCG
		}
	}

    
}