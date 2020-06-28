﻿Shader "Unlit/Character"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Gray("Gray", Range(0,1)) = 0
		_Hitted("Hitted", Range(0,1)) = 0
		_Hitted_Col("Hitted_Col", Color) = (1,0,0,1)
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog				

				#include "UnityCG.cginc"

				uniform fixed _Gray;
				fixed _Hitted;
				fixed4 _Hitted_Col;

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
					fixed4 col = tex2D(_MainTex, i.uv);
					// apply fog
					UNITY_APPLY_FOG(i.fogCoord, col);

					//color setting
					fixed3 finalColor = col;
					//set gray
					fixed gray = 0.2125 * col.r + 0.7154 * col.g + 0.0721 * col.b;
					fixed3 grayColor = fixed3(gray, gray, gray);
					finalColor = lerp(finalColor, grayColor, _Gray);

					//set red
					finalColor = lerp(finalColor, _Hitted_Col, _Hitted);

					return fixed4(finalColor, col.a);
				}
				ENDCG
			}
		}
}
