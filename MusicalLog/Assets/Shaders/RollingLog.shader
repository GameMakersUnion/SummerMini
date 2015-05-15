Shader "Custom/RollingLog" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_Radius ("Radius", Range(1,100)) = 1
	}
	SubShader {
		Pass {
			Tags { "RenderType"="Opaque" }
			CGPROGRAM
		
			// Compiler Directives
			#pragma exclude_renderers xbox360 ps3 flash
			#pragma vertex VS_MAIN
			#pragma fragment FS_MAIN
			
			// Predefined variables and helper functions (Unity specific)
			#include "UnityCG.cginc"
			
			// Uniform Variables (Properties)
			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Radius;
			
			// Input Structs
			struct FS_INPUT {
				float4 pos		: SV_POSITION;
				half2 uv		: TEXCOORD0;
			};
			
			// VERTEX FUNCTION
			FS_INPUT VS_MAIN (appdata_base input) {
				FS_INPUT output;
				
				float4 vert = mul(_Object2World, input.vertex);
				
				float dist = (vert.xyz - _WorldSpaceCameraPos).z;
				float curve = max(sqrt(pow(_Radius, 2) - pow((dist-_Radius), 2)) - _Radius, -_Radius);

				float vX = vert.x;
				float3 O = float3(vX, -_Radius, _WorldSpaceCameraPos.z + _Radius - vert.z);
				float3 P = float3(vX, curve, vert.z);
				vert.xyz = P-O * (vert.y / _Radius);
				vert.x = vX;
				
				input.vertex = mul (_World2Object, vert);
				// Setting FS_MAIN input struct
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
				output.uv = TRANSFORM_TEX(input.texcoord, _MainTex);
				
				return output;
			}
			
			// FRAGMENT FUNCTION
			fixed4 FS_MAIN (FS_INPUT input) : COLOR {
				return tex2D(_MainTex, input.uv) * _Color;
			}
		
			ENDCG
			
		}
	} 
	//FallBack "Diffuse"
}
