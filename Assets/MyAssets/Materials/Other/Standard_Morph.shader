// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Standard_Morpher"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (1,1,1,0)
		_MetallicRSmoothnessA("Metallic(R)Smoothness(A)", 2D) = "white" {}
		_Smoothness_intensity("Smoothness_intensity", Range( 0 , 1)) = 0
		_Metallic_intensity("Metallic_intensity", Range( 0 , 1)) = 0
		_VectorDisp("VectorDisp.", 2D) = "white" {}
		_VD_speed("VD_speed", Float) = 1
		_Morph_Intensity("Morph_Intensity", Float) = 1
		_Vertex_offset("Vertex_offset", Vector) = (0,0,0,0)
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 15
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 5.0
		#pragma surface surf Standard keepalpha noshadow vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _VectorDisp;
		uniform float _VD_speed;
		uniform float4 _VectorDisp_ST;
		uniform float _Morph_Intensity;
		uniform float3 _Vertex_offset;
		uniform float4 _Color;
		uniform sampler2D _MetallicRSmoothnessA;
		uniform float4 _MetallicRSmoothnessA_ST;
		uniform float _Metallic_intensity;
		uniform float _Smoothness_intensity;
		uniform float _EdgeLength;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float2 temp_cast_0 = (_VD_speed).xx;
			float2 uv0_VectorDisp = v.texcoord.xy * _VectorDisp_ST.xy + _VectorDisp_ST.zw;
			float2 panner147 = ( _Time.y * temp_cast_0 + uv0_VectorDisp);
			float3 ase_vertex3Pos = v.vertex.xyz;
			v.vertex.xyz += ( ( tex2Dlod( _VectorDisp, float4( panner147, 0, 0.0) ) * _Morph_Intensity ) + float4( ( ase_vertex3Pos + _Vertex_offset ) , 0.0 ) ).rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 temp_output_53_0 = _Color;
			o.Albedo = temp_output_53_0.rgb;
			o.Emission = temp_output_53_0.rgb;
			float2 uv_MetallicRSmoothnessA = i.uv_texcoord * _MetallicRSmoothnessA_ST.xy + _MetallicRSmoothnessA_ST.zw;
			float4 tex2DNode3 = tex2D( _MetallicRSmoothnessA, uv_MetallicRSmoothnessA );
			o.Metallic = ( tex2DNode3.r * _Metallic_intensity );
			o.Smoothness = ( tex2DNode3.a * _Smoothness_intensity );
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16600
303;73;1106;653;2200.101;1616.528;1.942;True;False
Node;AmplifyShaderEditor.CommentaryNode;24;-1306.028,741.978;Float;False;1226.477;945.6791;Comment;9;25;17;148;150;149;147;96;97;74;Vertex displacement;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;148;-1281.182,814.0155;Float;False;0;96;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;149;-1208.028,1018.848;Float;False;Property;_VD_speed;VD_speed;10;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;150;-1101.954,1127.36;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;25;-588.1437,1343.897;Float;False;471.9784;389.1577;position offset and normalization;3;23;9;22;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PannerNode;147;-991.9577,847.6623;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PosVertexDataNode;9;-522.3938,1393.897;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;74;-1157.422,1244.108;Float;False;Property;_Morph_Intensity;Morph_Intensity;12;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;96;-805.0053,854.9202;Float;True;Property;_VectorDisp;VectorDisp.;9;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;22;-538.144,1549.053;Float;False;Property;_Vertex_offset;Vertex_offset;13;0;Create;True;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;-462.5849,978.5964;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-783.603,-223.5475;Float;False;Property;_Metallic_intensity;Metallic_intensity;6;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-270.1654,1452.396;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;3;-800.1561,-152.8733;Float;True;Property;_MetallicRSmoothnessA;Metallic(R)Smoothness(A);4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-681.5814,35.03949;Float;False;Property;_Smoothness_intensity;Smoothness_intensity;5;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;173;-1755.587,-1134.632;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2.66,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;162;-1786.975,-1393.947;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2.66,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CosTime;163;-2464.078,-1491.431;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;170;-2172.392,-1219.6;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;165;-2188.271,-1413.81;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DotProductOpNode;172;-1953.161,-972.7581;Float;True;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;169;-1826.253,-1261.355;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2.66,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;176;-447.8915,138.4506;Float;False;Property;_Emission;Emission;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;159;-1468.946,-1403.325;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;174;-1470.411,-1209.269;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1199.332,-889.1887;Float;True;Property;_Albedo;Albedo;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;178;-281.9791,-693.9865;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenColorNode;80;-600.0133,-1591.725;Float;False;Global;_GrabScreen0;Grab Screen 0;17;0;Create;True;0;0;False;0;Object;-1;False;True;1;0;FLOAT4;0,0,0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;98;-216.8621,360.1064;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;167;-1466.952,-1298.674;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;177;-206.5328,119.0137;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;155;-1161.402,-1136.177;Float;True;COLOR;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-501.7435,-333.4924;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;89;-1089.534,-1787.649;Float;True;3;3;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-273.5912,-942.335;Float;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-412.5609,-233.4628;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;143;-1493.344,-1778.098;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;2;-1049.855,-562.4348;Float;True;Property;_Normals;Normals;7;0;Create;True;0;0;False;0;24d5cdc3119c63b4d96b3c0fa5570a7b;24d5cdc3119c63b4d96b3c0fa5570a7b;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinTimeNode;164;-2523.309,-1320.158;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;146;-775.805,-1631.893;Float;False;2;0;FLOAT;0;False;1;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-929.7498,1348.3;Half;False;Property;_Blend;Blend;11;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-449.0801,-56.52489;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;151;-1730.407,-492.2254;Float;False;0;2;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;152;-1298.843,-553.3168;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;53;-1203.405,-1380.814;Float;False;Property;_Color;Color;2;1;[HDR];Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;78;-1513.407,-1954.531;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;145;-870.0381,-1523.923;Float;False;Property;_Ior;Ior;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;144;-775.9182,-1830.647;Float;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;175;-1209.689,-272.719;Float;False;2;0;FLOAT;0;False;1;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-791.3386,-330.6389;Float;False;Property;_Normal_intensity;Normal_intensity;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;93.83333,-14.49531;Float;False;True;7;Float;ASEMaterialInspector;0;0;Standard;Standard_Morpher;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Translucent;5;True;False;0;True;Opaque;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;5;False;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;2;-1;3;14;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.3;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;147;0;148;0
WireConnection;147;2;149;0
WireConnection;147;1;150;0
WireConnection;96;1;147;0
WireConnection;97;0;96;0
WireConnection;97;1;74;0
WireConnection;23;0;9;0
WireConnection;23;1;22;0
WireConnection;173;0;172;0
WireConnection;162;0;165;0
WireConnection;170;0;164;1
WireConnection;170;1;163;1
WireConnection;165;0;163;1
WireConnection;165;1;164;1
WireConnection;172;0;165;0
WireConnection;172;1;170;0
WireConnection;169;0;170;0
WireConnection;159;0;162;0
WireConnection;174;0;173;0
WireConnection;178;0;53;0
WireConnection;178;1;155;0
WireConnection;178;2;1;0
WireConnection;80;0;144;0
WireConnection;98;0;97;0
WireConnection;98;1;23;0
WireConnection;167;0;169;0
WireConnection;177;0;178;0
WireConnection;177;1;176;0
WireConnection;155;0;159;0
WireConnection;155;1;167;0
WireConnection;155;2;174;0
WireConnection;33;0;2;0
WireConnection;33;1;34;0
WireConnection;89;0;78;0
WireConnection;89;1;143;0
WireConnection;89;2;33;0
WireConnection;73;0;80;0
WireConnection;73;1;53;0
WireConnection;73;2;155;0
WireConnection;73;3;1;0
WireConnection;5;0;3;1
WireConnection;5;1;4;0
WireConnection;2;1;152;0
WireConnection;146;0;145;0
WireConnection;6;0;3;4
WireConnection;6;1;7;0
WireConnection;152;0;151;0
WireConnection;152;2;175;0
WireConnection;152;1;150;0
WireConnection;144;0;78;0
WireConnection;144;1;89;0
WireConnection;144;2;146;0
WireConnection;175;0;149;0
WireConnection;0;0;53;0
WireConnection;0;2;53;0
WireConnection;0;3;5;0
WireConnection;0;4;6;0
WireConnection;0;11;98;0
ASEEND*/
//CHKSM=497D836C685EC03479F3487376B2A71D943ABCB2