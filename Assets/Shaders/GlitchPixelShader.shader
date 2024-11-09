Shader "Custom/GlitchPixelShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _GlitchAmount ("Glitch Amount", Range(0, 1)) = 0.2
        _PixelSize ("Pixel Size", Range(1, 16188)) = 2.0
        _MinGlitchCount ("Min Glitch Count", Range(1, 100)) = 5
        _MaxGlitchCount ("Max Glitch Count", Range(1, 100)) = 15
    }
    SubShader
    {
        Tags { "Queue" = "Background" }
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
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float _GlitchAmount;
            float _PixelSize;
            int _MinGlitchCount;
            int _MaxGlitchCount;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            // ������� ��� ���������� �����
            float4 frag(v2f i) : SV_Target
            {
                // ��������� ��������� ��������� ��� ��������� ����� ��������
                float2 screenCoord = i.uv;

                // ��������� ���������� ����� ��� ����������� �����
                float glitchProbability = random(screenCoord);

                // ���� ����������� ������ ��������� �����, ������� ��������� ������
                if (glitchProbability < _GlitchAmount)
                {
                    // ��������� ���������� ���������� �������� � �������� ���������
                    int glitchCount = _MinGlitchCount + int(random(screenCoord * 0.5) * float(_MaxGlitchCount - _MinGlitchCount));

                    // ��������� ��������� ������� ��� ������� �������� �������
                    float2 glitchPos = float2(random(screenCoord * 0.5), random(screenCoord * 0.7));

                    // ������� ������� � ������ ��� �������
                    float2 pixelCoord = floor(glitchPos * _PixelSize) / _PixelSize;

                    // ���������� �������� � ������ ������� ��������
                    if (length(screenCoord - pixelCoord) < (1.0 / _PixelSize))
                    {
                        return tex2D(_MainTex, pixelCoord);
                    }
                }
                
                return tex2D(_MainTex, screenCoord);
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
