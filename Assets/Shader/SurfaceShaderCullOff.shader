// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/DobleSidesDiffuse" { // 名前変更
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Front (RGB)", 2D) = "white" {} // Base → Front に変更
    _BackColor ("Back Color", Color) = (1,1,1,1)
    _BackMainTex("Back (RGB)", 2D) = "white" {} // _BackMainTex を追加
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 200

    // 裏面から描画
    Cull Front

    CGPROGRAM
    #pragma surface surf Lambert

    sampler2D _BackMainTex; // _MainTex → _BackMainTex に変更
    fixed4 _BackColor;

    struct Input {
        float2 uv_BackMainTex; // _MainTex → _BackMainTex に変更
    };

    void surf (Input IN, inout SurfaceOutput o) {
        fixed4 c = tex2D(_BackMainTex, IN.uv_BackMainTex) * _BackColor; // _MainTex → _BackMainTex に変更
        o.Albedo = c.rgb;
        o.Alpha = c.a;
    }
    ENDCG

    // 表面を描画（上記の CGPROGRAM から ENDCG を下記にコピーする
    Cull Back

    CGPROGRAM
    #pragma surface surf Lambert

    sampler2D _MainTex;
    fixed4 _Color;

    struct Input {
        float2 uv_MainTex;
    };

    void surf(Input IN, inout SurfaceOutput o) {
        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
        o.Albedo = c.rgb;
        o.Alpha = c.a;
    }
    ENDCG
}

Fallback "Nature/SpeedTree"
}