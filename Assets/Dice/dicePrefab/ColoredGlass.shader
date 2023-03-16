Shader "Custom/ColoredGlass"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _ThickColor ("Thick Glass Color", Color) = (0,0,0,1)
        _FakeThickness ("Fake Wall Thickness", Range(0,1)) = 0.5
        _MainTex ("Label Albedo & Alpha", 2D) = "black" {}
        _Glossiness ("Glass Smoothness", Range(0,1)) = 0.5
        _Metallic ("Glass Metallic", Range(0,1)) = 0.0
        _LabelGlossiness ("Label Smoothness", Range(0,1)) = 0.5
        _LabelMetallic ("Label Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
 
        // back face of glass & label
        Cull Front
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:premul
        #pragma target 3.0
 
        sampler2D _MainTex;
 
        struct Input
        {
            float2 uv_MainTex;
        };
 
        half _Glossiness, _LabelGlossiness;
        half _Metallic, _LabelMetallic;
        fixed4 _Color;
 
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed labelAlpha = c.a;
 
            o.Albedo = lerp(_Color.rgb, c.rgb, labelAlpha);
            o.Metallic = lerp(_Metallic, _LabelMetallic, labelAlpha);
            o.Smoothness = lerp(_Glossiness, _LabelGlossiness, labelAlpha);
            o.Alpha = lerp(_Color.a, 1.0, labelAlpha);
            o.Normal = half3(0,0,-1);
        }
        ENDCG
 
        // color tint pass
        Pass {
            Blend Zero SrcColor
            Cull Back
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };
 
            v2f vert (appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0));
                return o;
            }
 
            half4 _Color, _ThickColor;
            half _FakeThickness;
 
            half4 frag (v2f i) : SV_Target
            {
                float3 viewDir = -normalize(i.worldPos - _WorldSpaceCameraPos.xyz);
                float ndotv = dot(normalize(i.worldNormal), viewDir);
 
                float fresnel = pow(1.0 - saturate(ndotv), 2.0);
 
                // return fresnel;
 
                return lerp(_Color, _ThickColor, saturate(fresnel + _FakeThickness));
            }
            ENDCG
        }
 
        // front face of glass & label
        Cull Back
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:premul
        #pragma target 3.0
 
        sampler2D _MainTex;
 
        struct Input
        {
            float2 uv_MainTex;
        };
 
        half _Glossiness, _LabelGlossiness;
        half _Metallic, _LabelMetallic;
        fixed4 _Color;
 
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed labelAlpha = c.a;
 
            o.Albedo = lerp(_Color.rgb, c.rgb, labelAlpha);
            o.Metallic = lerp(_Metallic, _LabelMetallic, labelAlpha);
            o.Smoothness = lerp(_Glossiness, _LabelGlossiness, labelAlpha);
            o.Alpha = lerp(_Color.a, 1.0, labelAlpha);
        }
        ENDCG
    }
    FallBack "Diffuse"
}