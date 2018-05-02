Shader "Unlit/RayShader"
{
	// a single color property
    Properties {
        _Color ("Main Color", Color) = (1,.5,.5,1)
		_Tex("Main Tex", 2D) = "white" {}
    }
    // define one subshader
    SubShader
    {
		Tags{ "Queue" = "Transparent" }
        // a single pass in our subshader
        Pass
        {
			Cull Off
			ZWrite Off
			Blend One One
            Color[_Color]
			SetTexture[_Tex]{
				combine previous * texture
			}
        }
    }
}
