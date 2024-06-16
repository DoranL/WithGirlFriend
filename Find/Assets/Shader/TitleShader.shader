    Shader "Custom/TitleShader"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("NormalMap", 2D) = "bump"{}
        _GlossTex("GlossTex", 2D) = "White"{}

        _SpecCol("Specular Color", Color) = (1, 1, 1, 1)
        _SpecPow("Specular Power", Range(10, 200)) = 100

        _SpecCol2("Specular Color", Color) = (1, 1, 1, 1)
        _SpecPow2("Specular Power", Range(10, 200)) = 100
        
        _RimColor("Rim Color", Color) = (0.5, 0.3, 0.2, 1) 
        //3번의 스펙큘러 강도 조절이 잘 안되서 그런지 부자연스러움이 있어 색을 조정하여 그나마 자연스러워 보이도록 했다
        _RimPow("Rim Power", Range(1, 10)) = 7
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        CGPROGRAM
        #pragma surface surf Test 

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _GlossTex;
        float4 _SpecCol;
        float _SpecPow;
        float4 _SpecCol2;
        float _SpecPow2;
        float4 _RimColor;
        float _RimPow;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_GlossTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            float4 m = tex2D(_GlossTex, IN.uv_GlossTex);
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Gloss = m.a;
            o.Alpha = c.a;
        }

        float4 LightingTest(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten) 
        {
            float4 final;

            //Lambert영역
            float3 DiffColor;
            float ndot1 = saturate(dot(s.Normal, lightDir));
            DiffColor = ndot1 * s.Albedo * _LightColor0.rgb * atten;
            
            //Specular영역
            float3 SpecColor;
            float3 H = normalize(lightDir + viewDir);
            float spec = saturate(dot(H, s.Normal));
            spec = pow(spec, _SpecPow);
            SpecColor = spec * _SpecCol.rgb * s.Gloss;

            //Rim영역
            float3 rimColor;
            float rim = abs(dot(viewDir, s.Normal));
            float invrim = 1 - rim;
            rimColor = pow(invrim, _RimPow) * _RimColor.rgb;

            //Fake Specualr 영역
            float3 SpecColor2;
            SpecColor2 = pow(rim, _SpecPow2) * _SpecCol2 * s.Gloss;

            //final영역
            final.rgb = DiffColor.rgb + SpecColor.rgb + rimColor.rgb + SpecColor2.rgb;
            final.a = s.Alpha;
            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}