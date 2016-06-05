// AlterLayerColors.cs
// Creates a window which allows the user to edit plugin settings.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class AlterLayerColors : EditorWindow
{
	public static Color[] layerColors = ColorData.loadColorData();
	public bool useWireFrame = EditorPrefs.GetBool("useWireFrame", true);
	public bool useGlobalAlpha = EditorPrefs.GetBool("useGlobalAlpha", true);
	public float colorAlpha = EditorPrefs.GetFloat("colorAlpha", 0.5f);
	public float gizmoScale = EditorPrefs.GetFloat("gizmoScale", 1.05f);

	public bool foldSettings = true;
	public bool foldColors = true;

	public Vector2 scrollPosition = Vector2.zero;


	[MenuItem("Tools/Colorcode Layers/Alter Layer Colors %#g", false, 1050)]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow thisWindow = EditorWindow.GetWindow(typeof(AlterLayerColors), false, "Color Picker");
		thisWindow.minSize = new Vector2(300,300);
	}

	void OnGUI () 
	{
		// SETTINGS FOLD OUT

		foldSettings = EditorGUILayout.Foldout(foldSettings, "Settings");

		if(foldSettings)
		{
			useWireFrame = EditorGUILayout.Toggle ("Use Wireframe", useWireFrame);
			useGlobalAlpha = EditorGUILayout.Toggle ("useGlobalAlpha", useGlobalAlpha);
			if(useGlobalAlpha)
			{
				colorAlpha = EditorGUILayout.Slider ("Color Alpha", colorAlpha, 0.05f, 1.0f);
			}

			gizmoScale = EditorGUILayout.Slider ("Gizmo Scale", gizmoScale, 1.0f, 1.5f);
		}

		// COLORS FOLD OUT

		foldColors = EditorGUILayout.Foldout(foldColors, "Colors");

		if(foldColors)
		{
			scrollPosition = GUILayout.BeginScrollView(scrollPosition);
			for(int i=0; i<layerColors.Length; i++)
			{
				string LayerName = LayerMask.LayerToName(i);

				if(!string.IsNullOrEmpty(LayerName))
				{
					layerColors[i] = EditorGUILayout.ColorField(LayerName, layerColors[i]);
				}
			}
			GUILayout.EndScrollView();
		}

		// RANDOMIZE COLORS BUTTON

		if(GUILayout.Button("Randomize Colors"))
		{
			layerColors = ColorData.randomColorData();
			SceneView.RepaintAll();
		}
	}

	void OnDisable()
	{
		ColorData.saveColorData(layerColors);
		EditorPrefs.SetBool("useWireFrame", useWireFrame);
	}

	// If our inputed settings differ from our stored settings, update the stored settings.
	void OnInspectorUpdate()
	{
		if(EditorPrefs.GetBool("useWireFrame", true) != useWireFrame)
		{
			EditorPrefs.SetBool("useWireFrame", useWireFrame);
			SceneView.RepaintAll();
		}

		if(EditorPrefs.GetBool("useGlobalAlpha", true) != useGlobalAlpha)
		{
			EditorPrefs.SetBool("useGlobalAlpha", useGlobalAlpha);
			SceneView.RepaintAll();
		}

		if(EditorPrefs.GetFloat("colorAlpha", 0.5f) != colorAlpha)
		{
			EditorPrefs.SetFloat("colorAlpha", colorAlpha);
			SceneView.RepaintAll();
		}

		if(EditorPrefs.GetFloat("gizmoScale", 1.05f) != gizmoScale)
		{
			EditorPrefs.SetFloat("gizmoScale", gizmoScale);
			SceneView.RepaintAll();
		}
	}
}