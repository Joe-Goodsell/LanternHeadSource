
Shader "Unlit/NewSurfaceShader"
{
    Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _waveStrength ("_waveStrength", Range(0.01, 1.0)) = 0.5
        _spreadDistance ("_spreadDistance", Range(0.01, 1.0)) = 0.5
        // can produce ring shape if reduce the value
		_width ("_width", Range(0.01, 1.0)) = 0.5
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
            sampler2D _CameraSortingLayerTexture;
            float _waveStrength;
            float _spreadDistance;
            float _width;

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
				o.uv = v.uv;
		        o.screenPos = ComputeScreenPos(o.vertex);
				return o;
			}
			
			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				float2 center = float2(0.5, 0.5);
                float spreadDistance = abs(sin(_Time.y)) * _spreadDistance; // change the sin function for different spreading speed

				float len = length(v.uv - center);
                float outer_map = 1.0 - smoothstep(spreadDistance - _width, spreadDistance, len);
                float inner_map = smoothstep(spreadDistance - _width * 2.0, spreadDistance - _width, len);

                float map = outer_map * inner_map;

                float2 displacment = normalize(v.uv - center) * _waveStrength * map;
                float4 col = tex2D(_CameraSortingLayerTexture, v.screenPos - float4(displacment.xy, 0, 0));
				return col;
			}
			ENDCG
		}
	}
}