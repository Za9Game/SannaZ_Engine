sampler s0; 
texture lightMask; 
sampler lightSampler = sampler_state { Texture = <lightMask>; }; 

float4 PixelShaderLight(float2 coords: TEXCOORD0) : COLOR0
{ 
	float4 color = tex2D(s0, coords);
	float4 lightColor = tex2D(lightSampler, coords); 
	lightColor = lightColor * 1.4f;
	return color * lightColor; 
} 
technique Technique1 { 
	pass Pass1 { PixelShader = compile ps_4_0_level_9_1 PixelShaderLight(); }
}