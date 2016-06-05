// ColorData.cs
// Helper class that aids in serializing colors as groups of 4 floats.

using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class ColorData
{
	float[] rValues = new float[32];
	float[] gValues = new float[32];
	float[] bValues = new float[32];
	float[] aValues = new float[32];

	// Splits color variables into floats so that Unity can save and load them.
	public ColorData(Color[] colorArray)
	{
		for (int i=0; i<colorArray.Length; i++)
		{
			rValues[i] = colorArray[i].r;
			gValues[i] = colorArray[i].g;
			bValues[i] = colorArray[i].b;
			aValues[i] = colorArray[i].a;
		}
	}

	// Save settings to file.
	public static void saveColorData(Color[] colorsToSave)
	{
		ColorData colorData = new ColorData(colorsToSave);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/layerColorData.lcd");
		bf.Serialize(file, colorData);
		file.Close();
	}

	// Retrieve settings from file.
	public static Color[] loadColorData()
	{
		if(File.Exists(Application.persistentDataPath + "/layerColorData.lcd"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/layerColorData.lcd", FileMode.Open);
			ColorData colorData = (ColorData) bf.Deserialize(file);
			file.Close();

			return colorData.colorFromData();
		}
		else
		{
			return randomColorData();
		}
	}

	// Fills data with random colors.
	public static Color[] randomColorData()
	{
		Color[] colorArray = new Color[32];

		for(int i=0; i<colorArray.Length; i++)
		{
			colorArray[i] = new Color (Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
		}

		return colorArray;
	}

	// Returns color array from stored floats.
	public Color[] colorFromData()
	{
		Color[] temp = new Color[32];
		for (int i=0; i<temp.Length; i++)
		{
			temp[i] = new Color(rValues[i], gValues[i], bValues[i], aValues[i]);
		}
		return temp;
	}
}