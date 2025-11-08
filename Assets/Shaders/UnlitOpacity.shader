Shader "Custom/UnlitOpacityMask"
{
    Properties
    {
        _MainTex ("Main Texture (RGB)", 2D) = "white" {}
        _MaskTex ("Opacity Mask (R/G/B/A used as mask)", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)
        _Cutoff ("Mask Cutoff", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="TransparentCutout" "Queue"="AlphaTest" }
        LOD 100

        Pass
        {
            // Depth writes allowed for cutout objects (important for correct sorting)
            ZWrite On
            Cull Back
            Lighting Off
            // No blending for cutout
            Blend Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _MaskTex;
            fixed4 _Color;
            float _Cutoff;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uvMask : TEXCOORD1;
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uvMask = v.uv; // same UVs; change if mask uses another UV set
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                // Choose channel from mask; here we use mask.r but you can use .a for alpha channel
                float mask = tex2D(_MaskTex, i.uvMask).r;

                // Clip (discard) pixels where mask < cutoff
                clip(mask - _Cutoff);

                return col;
            }
            ENDCG
        }
    }

    // Fallback or editor-only settings
    FallBack "Unlit/Texture"
}
