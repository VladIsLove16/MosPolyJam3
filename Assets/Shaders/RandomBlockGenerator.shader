Shader "Custom/RandomBlockGenerator"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BlockSize ("Block Size", Range(1, 64)) = 8.0
        _TimeInterval ("Time Interval", Range(0.1, 5.0)) = 1.0
        _BlockDuration ("Block Duration", Range(0.1, 5.0)) = 1.0
        _BlockColor ("Block Color", Color) = (1, 0, 0, 1)
        _CustomTimeSinceStart ("Time Since Start", Float) = 0.0  // Renamed from _TimeSinceStart
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

            // ��������
            sampler2D _MainTex;
            float _BlockSize;
            float _TimeInterval;
            float _BlockDuration;
            float4 _BlockColor;
            float _CustomTimeSinceStart;  // Renamed from _TimeSinceStart

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // ������� ��������� ��������� �����
            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            // ������� ��� ��������� ������
            float4 frag(v2f i) : SV_Target
            {
                // ��������� ��������� ��������� ��� ��������� ������
                float2 screenCoord = i.uv;

                // ��������� ���������� ������� ��� ��������� �����
                float blockOffsetTime = random(screenCoord * 0.5) * _TimeInterval;

                // �����, ����� ���� ������ ���� �����
                float blockStartTime = floor(_CustomTimeSinceStart / _TimeInterval) * _TimeInterval + blockOffsetTime; // Changed _TimeSinceStart to _CustomTimeSinceStart
                float blockEndTime = blockStartTime + _BlockDuration;

                // ���� ������� ����� ��������� � �������� ������� �����
                if (_CustomTimeSinceStart > blockStartTime && _CustomTimeSinceStart < blockEndTime) // Changed _TimeSinceStart to _CustomTimeSinceStart
                {
                    // ��������� ��������� ������� ��� �����
                    float2 blockPos = float2(random(screenCoord * 0.3), random(screenCoord * 0.6));
                    blockPos = floor(blockPos * _BlockSize) / _BlockSize; // ���������� ������� � �����

                    // ���� ������� UV �������� � ������� �����, ���������� ����
                    if (length(screenCoord - blockPos) < (1.0 / _BlockSize))
                    {
                        return _BlockColor; // ���������� ���� �����
                    }
                }

                return tex2D(_MainTex, screenCoord); // ����� ���������� �������� ���������� ����
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
