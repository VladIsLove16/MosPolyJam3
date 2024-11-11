Shader "Custom/ScreenDistortionShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistortionStrength ("Distortion Strength", Float) = 0.1
        _DistortionSpeed ("Distortion Speed", Float) = 1.0
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

            sampler2D _MainTex;
            float _DistortionStrength;
            float _DistortionSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float2 WaveDistortion(float2 uv, float time)
            {
                float wave = sin(uv.y * 10.0 + time * _DistortionSpeed) * _DistortionStrength;
                return uv + float2(wave, 0.0);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time.y;
                
                // Применяем искажение
                float2 distortedUV = WaveDistortion(i.uv, time);
                
                // Получаем цвет из искаженной текстуры
                fixed4 color = tex2D(_MainTex, distortedUV);
                
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
