Shader "LineTest" {

Properties {
	_MainTex ("Particle Texture", 2D) = "white" {}
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend One OneMinusSrcAlpha
	ColorMask RGB
	Cull Off Lighting Off ZWrite On Fog { Mode Off }
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
		Bind "TexCoord", texcoord
	}
	

    SubShader {
        Pass {
            CGPROGRAM

            #pragma vertex vert_img
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
            

            fixed4 frag(float4 sp:WPOS, v2f_img i) : COLOR {
            	float4 texColor = tex2D(_MainTex, i.uv);
            
                return texColor;
            }

            ENDCG
        }
    }
  }
}