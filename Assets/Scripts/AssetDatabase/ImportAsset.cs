using UnityEditor;
using UnityEngine;

public class ImportAsset
{
    [MenuItem ("AssetDatabase/ImportExample")]
    static void ImportExample()
    {
        AssetDatabase.ImportAsset("Assets/Textures/texture.jpg", ImportAssetOptions.Default);
    }
}
