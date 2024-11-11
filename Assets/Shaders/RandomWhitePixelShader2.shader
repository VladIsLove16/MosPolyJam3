Shader "Custom/RandomWhitePixelShader2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ZoneSize ("Zone Size", Float) = 5.0
        _RandomOffset ("Random Offset", Vector) = (0, 0, 0, 0)
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
            float _ZoneSize;
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

            fixed4 frag (v2f i) : SV_Target
            {
                // Переводим координаты UV в пиксели экрана
                float2 pixelCoord = i.uv * _ScreenParams.xy;

                // Определяем случайное смещение для области
                float2 zoneStart = _RandomOffset * _ScreenParams.xy;

                // Проверяем, находится ли пиксель в пределах зоны
                bool isInWhiteZone = pixelCoord.x >= zoneStart.x && pixelCoord.x < (zoneStart.x + _ZoneSize) &&
                                     pixelCoord.y >= zoneStart.y && pixelCoord.y < (zoneStart.y + _ZoneSize);

                // Заливаем область белым, если пиксель в пределах зоны
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
