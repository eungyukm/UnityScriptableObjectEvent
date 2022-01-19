using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.Linq;

/// <summary>
/// GameEventManager는 
/// </summary>
public class GameEventManagerWindow : SearchableEditorWindow
{
    private List<GameEventListElement> projectEventList = new List<GameEventListElement>();
    private List<GameEventListener> listenerList = new List<GameEventListener>();

    private Vector2 scrollPos;

    private MultiColumnHeader projectColumnHeader;

    // Colum모음
    private MultiColumnHeaderState.Column[] projectColums;
    private MultiColumnHeaderState.Column[] sceneColums;

    private readonly Color lighterColor = Color.white * 0.3f;
    private readonly Color darkerColor = Color.white * 0.1f;

    private float columnsWidth;
    private const float column0MinWidth = 300;
    private const float column1MinWidth = 500;
    private const float column2MinWidth = 300;
    private const float column3MinWidth = 300;
    private const float column4MinWidth = 150;

    private int tabIndex = 0;
    private string[] tabStrings;
    private float columHeight;
    private float columnHeight;
    private SearchField searchField;
    private string searchString = string.Empty;
    private Rect boxRect;

    private void OnEnable()
    {
        InitializeColumnHeader();

        GetAssetList();

        SetWindow();
    }

    private void OnDestroy()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Column Header를 초기화 합니다.
    /// </summary>
    private void InitializeColumnHeader()
    {
        projectColums = new MultiColumnHeaderState.Column[]
        {
            new MultiColumnHeaderState.Column()
            {
                headerContent = new GUIContent("Game Event Asset"),
                width = column0MinWidth,
                minWidth = column0MinWidth,
                maxWidth = 400,
                autoResize = false,
                headerTextAlignment = TextAlignment.Center,
                canSort = true,
            },
            new MultiColumnHeaderState.Column()
            {
                headerContent = new GUIContent("Asset Path"),
                width = column0MinWidth,
                minWidth = column0MinWidth,
                maxWidth = 400,
                autoResize = false,
                headerTextAlignment = TextAlignment.Center,
                canSort = true,
            },
            new MultiColumnHeaderState.Column()
            {
                headerContent = new GUIContent("Description"),
                width = column2MinWidth,
                minWidth = column2MinWidth,
                maxWidth = 500,
                autoResize = false,
                headerTextAlignment = TextAlignment.Center,
                canSort = true,
            },
            new MultiColumnHeaderState.Column()
            {
                headerContent = new GUIContent("Function"),
                width = column4MinWidth,
                minWidth = column4MinWidth,
                maxWidth = 300,
                autoResize = false,
                headerTextAlignment = TextAlignment.Center,
                canSort = false,
            },
        };

        projectColumnHeader = new MultiColumnHeader(new MultiColumnHeaderState(projectColums));
        projectColumnHeader.height = 50;
        projectColumnHeader.visibleColumnsChanged += (MultiColumnHeader) => MultiColumnHeader.ResizeToFit();
        //projectColumnHeader.sortingChanged += (MultiColumnHeader) => SortAssetListInProject();
    }

    /// <summary>
    /// 게임 이벤트 에셋을 윈도우에 표시할 때 사용할 직렬화 클래스 리스트에 추가 합니다.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="asset"></param>
    private void Add(ref List<GameEventListElement> list, GameEvent asset)
    {
        GameEventListElement gameEventListElement = new GameEventListElement
        {
            gameEvent = asset,
            name = asset.name,
            assetPath = AssetDatabase.GetAssetPath(asset),
        };
        list.Add(gameEventListElement);
    }

    /// <summary>
    /// 윈도우 요소들의 기본값을 세팅합니다.
    /// </summary>
    private void SetWindow()
    {
        columnsWidth = column0MinWidth + column1MinWidth + column2MinWidth + column3MinWidth;

        Rect rect = position;
        rect.size = new Vector2(columnsWidth, 500);

        columnHeight = EditorGUIUtility.singleLineHeight * 2;

        tabStrings = new string[] { "Project", "Scene" };
        tabIndex = PlayerPrefs.GetInt("GameEventManger_TabIndex");


        // 퍼포먼스 하락을 방지하기 위한 코드
        autoRepaintOnSceneChange = false;
    }

    /// <summary>
    /// 프로젝트에 있는 모든 게임 이벤트 에셋을 가져 옵니다.
    /// </summary>
    private void GetAssetList()
    {
        GetAssetListInProject();
        GetAssetListInScene();
        Repaint();
    }

    /// <summary>
    /// 에셋 폴더 내의 에셋 리스트를 가져옵니다.
    /// </summary>
    private void GetAssetListInProject()
    {
        string[] assetGUIDList = AssetDatabase.FindAssets("t:GameEvent", null);

        projectEventList.Clear();
        foreach(var guid in assetGUIDList)
        {
            GameEvent asset = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid)) as GameEvent;
            Add(ref projectEventList, asset);
        }

        SortAssetListInProject();
    }

    /// <summary>
    /// 에셋 폴더 내의 에셋 리스트를 정렬 합니다.
    /// </summary>
    private void SortAssetListInProject()
    {
        switch (projectColumnHeader.sortedColumnIndex)
        {
            case 0:
                projectEventList = projectColumnHeader.IsSortedAscending(0)
                    ? projectEventList.OrderBy(asset => asset.gameEvent.name).ToList()
                    : projectEventList.OrderByDescending(asset => asset.gameEvent.name).ToList();
                break;

            case 1:
                projectEventList = projectColumnHeader.IsSortedAscending(1)
                    ? projectEventList.OrderBy(asset => AssetDatabase.GetAssetPath(asset.gameEvent)).ToList()
                    : projectEventList.OrderByDescending(asset => AssetDatabase.GetAssetPath(asset.gameEvent)).ToList();
                break;

            case 2:
                projectEventList = projectColumnHeader.IsSortedAscending(2)
                    ? projectEventList.OrderBy(aaset => aaset.gameEvent.description).ToList()
                    : projectEventList.OrderByDescending(asset => asset.gameEvent.description).ToList();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 현재 Scene의 에셋 리스트를 가져옵니다.
    /// </summary>
    private void GetAssetListInScene()
    {
        listenerList = GetAllListenerInScene();
    }

    /// <summary>
    /// 현재 Scene에 있는 모든 게임 이벤트 리스너를 가져와서 하이라키 순서로 정렬한 뒤 리턴합니다.
    /// </summary>
    /// <returns>현재 Scene에 있는 모든 게임 이벤트 리스너</returns>
    private static List<GameEventListener> GetAllListenerInScene()
    {
        GameEventListener[] gameEventListener = FindObjectsOfType<GameEventListener>().OrderBy(get => get.transform.GetSiblingIndex()).ToArray();

        return gameEventListener.Where(gel => gel.gameEvent).ToList();
    }

    //private void Add(ref)

    /// <summary>
    /// 게임 이벤트 매니저 메뉴를 표시합니다.
    /// </summary>
    [MenuItem("Contents/Game Event/Game Event Manager", priority = 0)]
    public static void ShowWindow()
    {
        GameEventManagerWindow window = GetWindow<GameEventManagerWindow>(false, "게임 이벤트 매니저", true);
        window.titleContent = new GUIContent("Game Event Manager");
    }

    [System.Serializable]
    public class GameEventListElement
    {
        public GameEvent gameEvent;
        public string name;
        public string assetPath;
    }
}
