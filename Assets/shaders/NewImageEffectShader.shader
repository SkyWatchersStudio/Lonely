Shader "Unlit/B&WEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ScreenPartitionWidth("ScreenPartitionWidth",  Range (0.0, 1.0)) = 0.5
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _ScreenPartitionWidth;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture

				fixed4 col = tex2D(_MainTex, i.uv);

				//Apply the perception brightness proportion for each color chanel
				float brightness = col.x * 0.3 + col.y * 0.59 + col.z *  0.11;

			     //If the uv x coordinate is higher than _ScreenPartitionWidth we apply the b&w effect, if not, we apply the image render how it is.
			    if(i.uv.x >_ScreenPartitionWidth)
			    {
			      //This condition is done in order to draw a vertical line which is the frontier between the image processed and the normal image
			        if(abs(i.uv.x -_ScreenPartitionWidth) < 0.005)
			        return fixed4(0.0,0.0,0.0,1.0);

			     	return fixed4(brightness,brightness,brightness,1.0) ;
			    }
			    else{
			      	return col;
			    }
				float  luminosity = col.x * 0.3 + col.y * 0.59 + col.z * 0.11;
				return fixed4(luminosity,luminosity,luminosity,1.0);
			}
			
		ENDCG
		}
	}
}
