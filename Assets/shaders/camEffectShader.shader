Shader "Unlit/B&WEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OldTVNoise ("Texture", 2D) = "white" {}
		_NoiseAttenuation ("NoiseAttenuation", Range(0.0 , 1.0)) = 0.5
		_Grainscale ("Grainscale", Range(0.0 , 10.0)) = 0.5
		_VigneteDistanceFormCenter("VigneteDistanceFormCenter", Range(0.0 , 10.0)) = 1.2
		_VignetteBlinkvelocity("VignetteBlinkvelocity", Range(0.0 , 10.0)) = 0.5
		_VignetteDarkAmount("VignetteDarkAmount", Range(0.0 , 10.0)) = 1.0
		_RandomNumber("RandomValue", Range(0.0 , 1.0)) = 1.0
		
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
			sampler2D _OldTVNoise;
			float4 _MainTex_ST;
			float _ScreenPartitionWidth;
			float _GrainScale;
			float _NoiseAttenuation;
			float _RandomNumber;
			float _VignetteBlinkvelocity;
			float _VignetteDarkAmount;
			float _VigneteDistanceFormCenter;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			float4 box(sampler2D tex, float2 uv,float4 size)
			{
				float4 c = tex2D(tex, uv + float2(-size.x , size.y)) + tex2D(tex , uv +float2(0 , size.y)) + 
							tex2D(tex , uv + float2(size.x , size.y)) + tex2D(tex , uv + float2(-size.x , 0)) + tex2D(tex , uv + float2(0 , 0)) + 
							tex2D(tex , uv + float2(size.x , 0)) + tex2D(tex , uv + float2(-size.x , -size.y)) + tex2D(tex , uv + float2(0 , -size.y)) + 
							tex2D(tex , uv + float2(size.x , -size.y));
				return c / 9;
			}
			
			
			fixed4 frag (v2f i) : SV_Target
			{
					// sample the texture

					fixed4 col = tex2D(_MainTex, i.uv);
				

			     //If the uv x coordinate is higher than _ScreenPartitionWidth we apply the b&w effect, if not, we apply the image render how it is.
			    if(i.uv.x >_ScreenPartitionWidth)
			    {
			      //This condition is done in order to draw a vertical line which is the frontier between the image processed and the normal image
			      if(abs(i.uv.x -_ScreenPartitionWidth) < 0.0005)
			     	return fixed4(0,0,0,1.0) ;

						//Apply the perception brightness proportion for each color chanel
						float brightness = col.x * 0.3 + col.y * 0.59 + col.z *  0.11;	

						fixed4 noise = clamp(fixed4(_NoiseAttenuation,_NoiseAttenuation,_NoiseAttenuation,1.0) + tex2D(_OldTVNoise, i.uv*_GrainScale + float2(_RandomNumber,_RandomNumber)), 0.0, 1.0);
						float fadeInBlack = pow(clamp(_VigneteDistanceFormCenter -distance(i.uv, float2(0.5,0.5)) +  abs(cos( _RandomNumber/10 +  _Time*10*_VignetteBlinkvelocity))/4, 0.0, 1.0),_VignetteDarkAmount);
						float4 blurCol = box(_MainTex, i.uv, float4(1.0,1.0,1.0,1.0));
						float blurValue = (blurCol.x * 0.3 + blurCol.y * 0.59 + blurCol.z *  0.11);
			  			return fixed4(brightness,brightness,brightness,1.0)/blurValue * noise * fadeInBlack*fadeInBlack * blurValue;
 				}
			    
			    else
				{
			      return col;
			  	  
				}
			
			}

			float Remap(float value, float from1, float to1, float from2, float to2)
			{
				return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
			}


			ENDCG
		}
	}
}
