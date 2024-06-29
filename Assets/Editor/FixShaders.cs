using UnityEngine;
using UnityEditor;

public class FixShaders : EditorWindow
{
    [MenuItem("Tools/Fix Shaders")]
    public static void ShowWindow()
    {
        GetWindow<FixShaders>("Fix Shaders");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Fix All Materials"))
        {
            FixMaterials();
        }
    }

    private void FixMaterials()
    {
        int fixedCount = 0;
        foreach (var material in Resources.FindObjectsOfTypeAll<Material>())
        {
            if (material.shader.name == "Hidden/InternalErrorShader")
            {
                material.shader = Shader.Find("Standard");
                EditorUtility.SetDirty(material);
                fixedCount++;
            }
        }

        Debug.Log($"Fix Complete! {fixedCount} materials fixed.");
        AssetDatabase.SaveAssets();
    }
}
