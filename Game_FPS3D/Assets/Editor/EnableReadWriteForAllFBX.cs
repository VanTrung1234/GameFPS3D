// Tạo file mới tại Assets/Editor/EnableReadWriteForAllFBX.cs
using UnityEngine;
using UnityEditor;

public class EnableReadWriteForAllFBX : EditorWindow
{
    [MenuItem("Tools/Enable Read/Write for all FBX models")]
    static void EnableReadWrite()
    {
        string[] guids = AssetDatabase.FindAssets("t:Model");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;

            if (importer != null && !importer.isReadable)
            {
                importer.isReadable = true;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                Debug.Log("Enabled Read/Write on: " + path);
            }
        }
        Debug.Log("Done enabling Read/Write on all FBX models.");
    }
}
