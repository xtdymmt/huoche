// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.Extensions.MeshFilterExt
using System;
using UnityEngine;

namespace FluffyUnderware.DevTools.Extensions
{
	public static class MeshFilterExt
	{
		public static Mesh PrepareNewShared(this MeshFilter m, string name = "Mesh")
		{
			if (m == null)
			{
				return null;
			}
			if (m.sharedMesh == null)
			{
				Mesh mesh = new Mesh();
				mesh.MarkDynamic();
				mesh.name = name;
				m.sharedMesh = mesh;
			}
			else
			{
				m.sharedMesh.Clear();
				m.sharedMesh.name = name;
				m.sharedMesh.subMeshCount = 0;
			}
			return m.sharedMesh;
		}

		public static void CalculateTangents(this MeshFilter m)
		{
			int[] triangles = m.sharedMesh.triangles;
			Vector3[] vertices = m.sharedMesh.vertices;
			Vector2[] uv = m.sharedMesh.uv;
			Vector3[] normals = m.sharedMesh.normals;
			if (uv.Length == 0)
			{
				return;
			}
			int num = triangles.Length;
			int num2 = vertices.Length;
			Vector3[] array = new Vector3[num2];
			Vector3[] array2 = new Vector3[num2];
			Vector4[] array3 = new Vector4[num2];
			for (int i = 0; i < num; i += 3)
			{
				int num3 = triangles[i];
				int num4 = triangles[i + 1];
				int num5 = triangles[i + 2];
				Vector3 vector = vertices[num3];
				Vector3 vector2 = vertices[num4];
				Vector3 vector3 = vertices[num5];
				Vector2 vector4 = uv[num3];
				Vector2 vector5 = uv[num4];
				Vector2 vector6 = uv[num5];
				float num6 = vector2.x - vector.x;
				float num7 = vector3.x - vector.x;
				float num8 = vector2.y - vector.y;
				float num9 = vector3.y - vector.y;
				float num10 = vector2.z - vector.z;
				float num11 = vector3.z - vector.z;
				float num12 = vector5.x - vector4.x;
				float num13 = vector6.x - vector4.x;
				float num14 = vector5.y - vector4.y;
				float num15 = vector6.y - vector4.y;
				float num16 = num12 * num15 - num13 * num14;
				float num17 = (num16 != 0f) ? (1f / num16) : 0f;
				float num18 = (num15 * num6 - num14 * num7) * num17;
				float num19 = (num15 * num8 - num14 * num9) * num17;
				float num20 = (num15 * num10 - num14 * num11) * num17;
				float num21 = (num12 * num7 - num13 * num6) * num17;
				float num22 = (num12 * num9 - num13 * num8) * num17;
				float num23 = (num12 * num11 - num13 * num10) * num17;
				Vector3[] array4 = array;
				int num24 = num3;
				array4[num24].x = array4[num24].x + num18;
				Vector3[] array5 = array;
				int num25 = num3;
				array5[num25].y = array5[num25].y + num19;
				Vector3[] array6 = array;
				int num26 = num3;
				array6[num26].z = array6[num26].z + num20;
				Vector3[] array7 = array;
				int num27 = num4;
				array7[num27].x = array7[num27].x + num18;
				Vector3[] array8 = array;
				int num28 = num4;
				array8[num28].y = array8[num28].y + num19;
				Vector3[] array9 = array;
				int num29 = num4;
				array9[num29].z = array9[num29].z + num20;
				Vector3[] array10 = array;
				int num30 = num5;
				array10[num30].x = array10[num30].x + num18;
				Vector3[] array11 = array;
				int num31 = num5;
				array11[num31].y = array11[num31].y + num19;
				Vector3[] array12 = array;
				int num32 = num5;
				array12[num32].z = array12[num32].z + num20;
				Vector3[] array13 = array2;
				int num33 = num3;
				array13[num33].x = array13[num33].x + num21;
				Vector3[] array14 = array2;
				int num34 = num3;
				array14[num34].y = array14[num34].y + num22;
				Vector3[] array15 = array2;
				int num35 = num3;
				array15[num35].z = array15[num35].z + num23;
				Vector3[] array16 = array2;
				int num36 = num4;
				array16[num36].x = array16[num36].x + num21;
				Vector3[] array17 = array2;
				int num37 = num4;
				array17[num37].y = array17[num37].y + num22;
				Vector3[] array18 = array2;
				int num38 = num4;
				array18[num38].z = array18[num38].z + num23;
				Vector3[] array19 = array2;
				int num39 = num5;
				array19[num39].x = array19[num39].x + num21;
				Vector3[] array20 = array2;
				int num40 = num5;
				array20[num40].y = array20[num40].y + num22;
				Vector3[] array21 = array2;
				int num41 = num5;
				array21[num41].z = array21[num41].z + num23;
			}
			for (int j = 0; j < num2; j++)
			{
				Vector3 vector7 = normals[j];
				Vector3 vector8 = array[j];
				Vector3.OrthoNormalize(ref vector7, ref vector8);
				array3[j].x = vector8.x;
				array3[j].y = vector8.y;
				array3[j].z = vector8.z;
				float num42 = (vector7.y * vector8.z - vector7.z * vector8.y) * array2[j].x + (vector7.z * vector8.x - vector7.x * vector8.z) * array2[j].y + (vector7.x * vector8.y - vector7.y * vector8.x) * array2[j].z;
				array3[j].w = ((num42 >= 0f) ? 1f : -1f);
			}
			m.sharedMesh.tangents = array3;
		}
	}
}
