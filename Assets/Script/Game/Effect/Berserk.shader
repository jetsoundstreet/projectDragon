// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Berserk"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Size ("Size", Float) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Size;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				float a = atan2(i.uv.y - 0.5, i.uv.x - 0.5);
				float d = clamp((distance(float2(0.5, 0.5), i.uv) + sin(a * 10) * 0.05 + sin(a * 30) * 0.01 + 1 -_Size * 2) * 2, 0, 1);
				col = (d * col + (1 - d) * (1 - col));
				return col;
			}
			ENDCG
		}
	}
}
