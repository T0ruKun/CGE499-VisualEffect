Shader "CGE499/Toon" {
	Properties {
		_Color			("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex		("Main Texture", 2D) = "white" {}	
		[HDR] 
		_AmbientColor	("Ambient Color", Color) = (0.4, 0.4, 0.4, 1)
		[HDR] 
		_SpecularColor	("Specular Color", Color) = (.9, .9, .9, 1)
		_Glossiness		("Glossiness", Float) = 32
		[HDR] 
	    _RimColor		("Rim Color", Color) = (1, 1, 1, 1)
		_RimAmount		("Rim Amount", Range(0, 1)) = .716 
		_RimThreshold	("Rim Threshold", Range(0, 1)) = .1
	}
	SubShader {
		Tags {
			"RenderType" = "Opaque"
			"RenderPipeline" = "UniversalPipeline"
			"IgnoreProjector" = "True"
		}
		
		Pass {
			Tags {"LightMode" = "UniversalForwardOnly""PassFlags" = "OnlyDirectional"}
			LOD 300

			CGPROGRAM
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			struct appdata {
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1;
				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _AmbientColor;
			float _Glossiness;
			float4 _SpecularColor;
			float4 _RimColor;
			float4 _RimAmount;
			float _RimThreshold;
			
			v2f vert (appdata data) {
				v2f o;
				o.pos = UnityObjectToClipPos(data.vertex);
				o.uv = TRANSFORM_TEX(data.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(data.normal);
				o.viewDir = WorldSpaceViewDir(data.vertex);
				TRANSFER_SHADOW(o)
				return o;
			}
			
			float4 _Color;

			float4 frag (v2f i) : SV_Target {
				float3 normal = normalize(i.worldNormal);
				float3 viewDir = normalize(i.viewDir);

				//------------------------------------------------------------------------------------------------------
				// direction of the main directional light.
				float NdotL = dot(_WorldSpaceLightPos0, normal);
				float lightIntensity = smoothstep(0, 0.01, NdotL);
				float4 light = lightIntensity * _LightColor0;

				//------------------------------------------------------------------------------------------------------
				// Calculate specular reflection.
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);
				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;				

				//------------------------------------------------------------------------------------------------------
				// Calculate rim lighting.
				float rimDot = 1 - dot(viewDir, normal);
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float4 rim = rimIntensity * _RimColor;

				//------------------------------------------------------------------------------------------------------
				// Main Tex
				float4 sample = tex2D(_MainTex, i.uv);
				return (light + _AmbientColor + specular + rim) * _Color * sample;
			}
			
			ENDCG
		}
	}
}