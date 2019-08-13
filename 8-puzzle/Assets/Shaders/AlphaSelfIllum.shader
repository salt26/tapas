Shader "Transparent/AlphaSelfIllum"
{
	Properties{
		[HDR]_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		SubShader{
			Tags {"RenderType" = "Transparent"}
			LOD 200

			CGPROGRAM
			#pragma surface surf Lambert alpha

			sampler2D _MainTex;
			fixed4 _Color;

			struct Input {
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = (0, 0, 0, 0);
				o.Emission = c.rgb;
				o.Alpha = c.rgb;
			}
			ENDCG
		}

			Fallback "Transparent/VertexLit"
}

