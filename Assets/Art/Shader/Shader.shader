Shader "Shaders/Movement"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _Velocity ("Velocity", Float) = 0
        _WaveLength("Wave Length", Float) = 0
        _Pow("Power", Range(0,0.15)) = 0
        _Speed("Speed", Float) = 0
        _UnityTime("UnityTime", Float) = 0

    }

   SubShader 
	{	
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			struct appdata 
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f 
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
			};
			
			sampler2D _MainTex;
			float _Velocity;
			float _Pow;
			float _WaveLength;
			float _Speed;
			float _UnityTime;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{					

               /*  float2 result = i.texcoord;
                result.x = result.x + sin((result.y+(_Time.y * _Speed)) * _WaveLength) * _Pow;*/
				
				float2 uv = i.texcoord; // * 1.2 para hacer q se baje la texture
                uv.x += 0.1 * (sin(3 * (uv.y + (_Time.x - _UnityTime)))) * _Velocity; //de forma ONDULADA
				uv.x += (_Time.x - _UnityTime) * _Velocity; // de forma horizontal
				
				fixed4 col = tex2D(_MainTex, uv);
							
				return col ;

			}
			ENDCG
		}
    }
}