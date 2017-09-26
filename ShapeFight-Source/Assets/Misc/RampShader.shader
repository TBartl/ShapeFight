Shader "Skin/Skin03-Ramp1D"
{
	Properties{
		_Ramp("Ramp", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader{
		Pass
		{
			Tags{ "LightMode" = "ForwardBase" } // So diffuse lighting will always face the correct direction
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc" // For UnityObjectToWorldNormal

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			struct v2f
			{
				float4 vertex : SV_POSITION;
				fixed4 diffuse : COLOR0;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				o.diffuse = (dot(worldNormal, _WorldSpaceLightPos0.xyz) + 1) / 2;
				return o;
			}

			sampler2D _Ramp;
			float4 _Color;
				
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_Ramp, i.diffuse) * _Color;
				return col;
			}

			ENDCG			
		}
	}
}
