using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveFilePanelInProjectExample : EditorWindow
{
    [MenuItem("Contents/GameEvent/SaveFilePanelInProject")]
    static void Apply()
    {
        Texture2D texture = Selection.activeObject as Texture2D;
        if (texture == null)
        {
            EditorUtility.DisplayDialog("Select Texture", "You must select a texture first!", "OK");
            return;
        }

        string path = EditorUtility.SaveFilePanelInProject("Svae png", texture.name + "png", "png", "Please enter a file name to save the texture to");

        if(path.Length != 0)
        {
            byte[] pngData = texture.EncodeToPNG();
            if (pngData != null)
            {
                File.WriteAllBytes(path, pngData);

                AssetDatabase.Refresh();
            }
        }    
    }
}
