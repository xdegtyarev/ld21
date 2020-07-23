// unlit, vertex colour, alpha blended
// cull off

Shader "VC" 
{
	SubShader
	{
		Tags {"IgnoreProjector"="True" "RenderType"="Opaque"}
		Blend Off Lighting Off Cull Off Fog { Mode Off }
		
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag 
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			struct vdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			vdata vert(inout vdata i)
			{
				i.vertex = mul(UNITY_MATRIX_MVP, i.vertex);
				return i;
			}

			fixed4 frag(vdata i) : COLOR
			{
				return i.color;
			}
			
			ENDCG
		} 
	}
 }
	SubShader 
	{
		Tags {"IgnoreProjector"="True" "RenderType"="Opaque"}
		Blend Off Cull Off Fog { Mode Off }
		LOD 100
		
		BindChannels 
		{
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
			Bind "Color", color
		}

		Pass 
		{
			Lighting Off
			SetTexture [_MainTex] { combine texture * primary } 
		}
	}
}
