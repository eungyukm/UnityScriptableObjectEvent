using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectExample : ScriptableObject
{
    public int age;

    [MenuItem("Contents/GameEvent/CreateAssetExample")]
    public static void CreateAsset()
    {
        ScriptableObjectExample asset = CreateInstance<ScriptableObjectExample>();
        AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/CreateExampleSO.asset");
        AssetDatabase.SaveAssets();
    }
}