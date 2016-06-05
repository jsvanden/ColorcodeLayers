// ToggleLayerColors.cs
// Enables/disables drawing gizmos for each mesh.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class LayerColorMenu : MonoBehaviour
{
	[MenuItem("Tools/Colorcode Layers/Toggle Layer Colors %g", false, 1050)]
	public static void ToggleLayerColors ()
	{
		if(EditorPrefs.GetBool("showColors", false))
		{
			EditorPrefs.SetBool("showColors", false);
		}
		else
		{
			EditorPrefs.SetBool("showColors", true);
		}

		SceneView.RepaintAll();
	}
}