// DrawLayerColors.cs
// Draws Gizmos of each object with a mesh (colorcodes based on layer).

using System;
using UnityEngine;
using UnityEditor;
using System.Collections;

public class DrawLayerColors : Editor
{
	// STATIC MESH PASS
	[DrawGizmo(GizmoType.Active | GizmoType.NotInSelectionHierarchy | GizmoType.InSelectionHierarchy)]
	static void DrawMeshGizmos(MeshFilter temp, GizmoType gizmoType)
	{
		if(EditorPrefs.GetBool("showColors", false))
		{
			Matrix4x4 rotationMatrix = Matrix4x4.TRS(temp.transform.position, temp.transform.rotation, temp.transform.lossyScale);
			Gizmos.matrix = rotationMatrix;

			Color layerColor = AlterLayerColors.layerColors[temp.gameObject.layer];
			if (EditorPrefs.GetBool("useGlobalAlpha", true))
			{
				layerColor.a = EditorPrefs.GetFloat("colorAlpha", 0.5f);
			}
			Gizmos.color = layerColor;

			Mesh tempMesh = temp.sharedMesh;
			float scale = EditorPrefs.GetFloat("gizmoScale", 1.05f);
			Vector3 tempScale = new Vector3(scale, scale, scale);

			if(EditorPrefs.GetBool("useWireFrame", true))
			{
				Gizmos.DrawWireMesh(tempMesh, Vector3.zero, Quaternion.identity, tempScale);
			}
			else
			{
				Gizmos.DrawMesh(tempMesh, Vector3.zero, Quaternion.identity, tempScale);
			}
		}
	}

	// SKINNED MESH PASS
	[DrawGizmo(GizmoType.Active | GizmoType.NotInSelectionHierarchy | GizmoType.InSelectionHierarchy)]
	static void DrawSkinnedMeshGizmos(SkinnedMeshRenderer temp, GizmoType gizmoType)
	{
		if(EditorPrefs.GetBool("showColors", false))
		{
			Matrix4x4 rotationMatrix = Matrix4x4.TRS(temp.transform.position, temp.transform.rotation, temp.transform.lossyScale/1.5f);
			Gizmos.matrix = rotationMatrix;

			Color layerColor = AlterLayerColors.layerColors[temp.gameObject.layer];
			if (EditorPrefs.GetBool("useGlobalAlpha", true))
			{
				layerColor.a = EditorPrefs.GetFloat("colorAlpha", 0.5f);
			}
			Gizmos.color = layerColor;

			Mesh tempMesh = temp.sharedMesh;
			float scale = EditorPrefs.GetFloat("gizmoScale", 1.05f) * 1.5f;
			Vector3 tempScale = new Vector3(scale, scale, scale);

			if(EditorPrefs.GetBool("useWireFrame", true))
			{
				Gizmos.DrawWireMesh(tempMesh, Vector3.zero, Quaternion.identity, tempScale);
			}
			else
			{
				Gizmos.DrawMesh(tempMesh, Vector3.zero, Quaternion.identity, tempScale);
			}
		}
	}
}