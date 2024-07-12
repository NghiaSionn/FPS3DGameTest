using UnityEngine;
using UnityEditor;

public class ConvertURPToStandard : EditorWindow
{
    [MenuItem("Tools/Convert URP to Standard Shader")]
    public static void ShowWindow()
    {
        GetWindow<ConvertURPToStandard>("Convert URP to Standard Shader");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Convert All Materials"))
        {
            ConvertMaterials();
        }
    }

    private void ConvertMaterials()
    {
        int convertedCount = 0;
        foreach (var material in Resources.FindObjectsOfTypeAll<Material>())
        {
            if (material.shader.name.StartsWith("Universal Render Pipeline"))
            {
                material.shader = Shader.Find("Standard");
                EditorUtility.SetDirty(material);
                convertedCount++;
            }
        }

        Debug.Log($"Conversion Complete! {convertedCount} materials converted.");
        AssetDatabase.SaveAssets();
    }
}
