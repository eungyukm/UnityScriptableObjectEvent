using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameEventDescriptionOption : ScriptableObject
{
    public int fontSize = 15;
    public bool locked = true;
    public int tyingAreaHeight = 12;

    [MenuItem("Contents/GameEvent/Option", priority = 1)]
    public static void ShowAsset()
    {
        // GameEventDescriptionOption을 이용하여, Game의 설명을 할 수 있습니다.
        string[] assetGUIDList = AssetDatabase.FindAssets("t:GameEventDescriptionOption", null);

        Debug.Log("asset guid Length : " + assetGUIDList.Length);

        if(assetGUIDList.Length > 0)
        {
            Debug.Log("설명 옵션이 이미 존재합니다.");
            string path = AssetDatabase.GUIDToAssetPath(assetGUIDList[0]);
            Debug.Log("GameEventDescriptionOption path : " + path);
            Object asset = AssetDatabase.LoadMainAssetAtPath(path);
            // 선택된 핑을 해주는 것
            EditorGUIUtility.PingObject(asset);
            // 선택된 오브젝트 Inspector에 표시하는 역할을 함
            AssetDatabase.OpenAsset(asset);
        }
        else
        {
            CreateAsset();
        }
    }

    public static void CreateAsset()
    {
        var path = EditorUtility.SaveFilePanelInProject("설명 옵션 파일 저장", "GameEventDescriptionOption", "asset", "");

        if (path.Equals(string.Empty))
        {
            return;
        }

        GameEventDescriptionOption asset = ScriptableObject.CreateInstance<GameEventDescriptionOption>();
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        EditorGUIUtility.PingObject(asset);
        AssetDatabase.OpenAsset(asset);
    }
}
