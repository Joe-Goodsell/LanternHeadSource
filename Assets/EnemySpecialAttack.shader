Shader "Unlit/EnemyAttackEffect"
{
    Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _waveStrength ("_waveStrength", Range(0.01, 1.0)) = 0.25
        _spreadDistance ("_spreadDistance", Range(0.01, 1.0)) = 0.5
		_width ("_width", Range(0.01, 1.0)) = 0.8
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
                float spreadDistance = 0.2 + abs(sin(_Time.y / 1.5) * _spreadDistance); // change the sin function for different spreading speed
				// float spreadDistance = _spreadDistance;
				float len = length(v.uv - center);
                float map = 1.0 - smoothstep(spreadDistance - _width, spreadDistance, len);
				float map_2 = smoothstep(spreadDistance + _width, spreadDistance, len);

                float2 displacment = normalize(v.uv - center) * _waveStrength * map * map_2;
                float4 col = tex2D(_CameraSortingLayerTexture, v.screenPos - float4(displacment.xy, 0, 0));
				return col;
			}
			ENDCG
		}
	}
}