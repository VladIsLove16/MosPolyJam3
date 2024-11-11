Shader "Custom/RandomWhitePixelShaderUS"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Interval ("Interval", Float) = 1.0
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
            float _Interval;
            float2 _RandomOffset;

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

            float rand(float2 co)
            {
                return frac(sin(dot(co, float2(12.9898,78.233))) * 43758.5453);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Получаем текущее время
                float time = _Time.y;

                // Случайное смещение для зоны
                float2 offset = float2(rand(float2(time, time * 2.0)), rand(float2(time * 3.0, time * 4.0)));

                // Определяем, находится ли пиксель в пределах зоны 5x5
                float2 uvZone = floor(i.uv * 100.0 + offset * 100.0) / 100.0;
                bool isInWhiteZone = abs(uvZone.x - offset.x) < 0.05 && abs(uvZone.y - offset.y) < 0.05;

                // Меняем цвет на белый, если пиксель в пределах зоны
                if (isInWhiteZone)
                    return fixed4(1, 1, 1, 1); // Белый цвет
                else
                    return tex2D(_MainTex, i.uv); // Исходный цвет
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
