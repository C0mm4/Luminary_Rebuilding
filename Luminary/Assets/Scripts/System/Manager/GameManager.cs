using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static GameManager gm_Instance;
    public static GameManager Instance
    {
        get
        {
            // if instance is NULL create instance
            if (!gm_Instance)
            {
                gm_Instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (gm_Instance == null)
                    Debug.Log("instance is NULL_GameManager");
            }
            return gm_Instance;
        }
    }
    public static SceneTransition sceneTransition;
    public static CameraManager cameraManager;
    [SerializeField]
    public static PlayerDataManager playerDataManager;
    public static GameObject player;
    public static InputManager inputManager;

    public static GameState gameState;
    [SerializeField]
    public static UIState uiState = UIState.Title;

    public Action SceneChangeAction;

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return gm_Instance._resource; } }

    MapGen _mapGen = new MapGen();
    public static MapGen MapGen { get { return gm_Instance._mapGen; } }

    RandomEncounter _randomEncounter = new RandomEncounter();
    public static RandomEncounter Random { get { return gm_Instance._randomEncounter; } }

    StageController _stageController = new StageController();
    public static StageController StageC { get { return gm_Instance._stageController; } }

    SpellManager _spellManager = new SpellManager();
    public static SpellManager Spells { get { return gm_Instance._spellManager; } }

    FSMManager _fsmManager = new FSMManager();
    public static FSMManager FSM { get { return gm_Instance._fsmManager; } }

    ScriptManager _scriptManager = new ScriptManager();
    public static ScriptManager Script { get { return gm_Instance._scriptManager; } }

    public struct SerializedGameData
    {
        public List<Resolution> resolutionList;
        // �ػ� ���
        public Resolution resolution;
        // ���� �ػ�
        public bool isFullscreen;
        public float gameVolume;
        // ��ü ����
        public float musicVolume;
        // ������� ����
        public float effectVolume;
        // ȿ���� ����
    }

    public static SerializedGameData gameData;
    public GameObject persistentObject;
    // == this

    public AudioSource audioSourceBGM;
    private bool isPaused = false;
    // �ý��� ����

    
    [SerializeField]
    public Canvas canvas;
    [SerializeField]
    public UIManager uiManager;
    [SerializeField]
    public static ItemDataManager itemDataManager;
    public static MobSpawnner mobSpawnner;

    private void Awake()
    {
        // Find System Components
        DontDestroyOnLoad(persistentObject);
        gm_Instance = this;
        sceneTransition = GameObject.Find("GameManager").GetComponent<SceneTransition>();
        cameraManager = GameObject.Find("GameManager").GetComponent<CameraManager>();
        playerDataManager = GameObject.Find("GameManager").GetComponent<PlayerDataManager>();
        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
        bool isFirstRun = PlayerPrefs.GetInt("isFirstRun", 1) == 1;
        if (isFirstRun)
        {
            // is first
        }
        else
        {
            // else
        }


        //reset game
        loadData();
        Screen.SetResolution(gameData.resolution.width, gameData.resolution.height, gameData.isFullscreen);

        // Set Canvas
        if(canvas == null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            if(canvas.gameObject.GetComponent<UIManager>() == null)
            {
                canvas.gameObject.AddComponent<UIManager>();
            }

            uiManager = canvas.GetComponent<UIManager>();


            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            canvas.planeDistance = 7;
        }

        Application.targetFrameRate = 60;
        SceneChangeAction += GameObjectReSet;

        // set Item data, mob Datas
        if(itemDataManager == null)
        {
            itemDataManager = gameObject.GetComponent<ItemDataManager>();
            itemDataManager.Init();
        }

        if(mobSpawnner == null)
        {
            mobSpawnner = gameObject.GetComponent<MobSpawnner>();
            mobSpawnner.init();
        }
        init();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // if canvas not initialize, init canvas
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            if (canvas.gameObject.GetComponent<UIManager>() == null)
            {
                canvas.gameObject.AddComponent<UIManager>();
            }
            uiManager = canvas.GetComponent<UIManager>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            canvas.planeDistance = 7;
        }

    }
    // Game Object All Destroy
    public void GameObjectReSet()
    {
        Resource.Destroy(GameObject.FindGameObjectsWithTag("Player"));
    }
    // Initialize System Components
    public void init()
    {
        Debug.Log("GameManager Awake Init");
        // Spell ��ü�� �ε��ϰ� ����� ��ü �ʱ�ȭ
        Spells.init();
        FSM.init();
        Random.init("");
        MapGen.init();
        StageC.init();
        Script.init();
        playerDataManager.playerDataInit();
    }

    // Loading Setting datas
    public void loadData()
    {
        playerDataManager.loadKeySetting();

        gameData.resolution.width = PlayerPrefs.GetInt("resolutionW", Screen.currentResolution.width);
        gameData.resolution.height = PlayerPrefs.GetInt("resolutionH", Screen.currentResolution.height);
        gameData.isFullscreen = PlayerPrefs.GetInt("isFullscreen", Screen.fullScreen ? 1 : 0) == 1;
        // load resolution

        gameData.gameVolume = PlayerPrefs.GetFloat("gameVolume", 0.3f);
        gameData.effectVolume = PlayerPrefs.GetFloat("effectVolume", 0.3f);
        gameData.musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.3f);
        // load volume
    }

    public void saveData()
    {

    }

    
    public void sceneControl(string targetScene)
    {
        gameState = GameState.Loading;
        SceneChangeAction?.Invoke();
        
        sceneTransition.sceneLoad(targetScene);
    }


    public void transitionInit(string targetScene)
    {
        sceneSetClear();
        setCanvas();
        switch (targetScene)
        {
            case "LobbyScene":
                lobbySceneInit();
                break;
            case "StageScene":
                stageSceneInit();
                break;
            case "TutorialScene":
                tutorialSceneInit();
                break;
            default:
                break;
        }
    }

    // Clear Interaction Object data
    public void sceneSetClear()
    {
        PlayerDataManager.interactionObject = null;
        PlayerDataManager.interactionDistance = 5.5f;
    }

    // Lobby Scene Initialize
    public void lobbySceneInit()
    {
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        SpriteRenderer lobbyField = GameObject.Find("LobbyField").GetComponent<SpriteRenderer>();
        cameraManager.camera = mainCamera;
        cameraManager.background = lobbyField;
        playerGen();
        uiManager.ChangeState(UIState.Lobby);
        gameState = GameState.InPlay;

        Resource.Instantiate("Mobs/Goblin/Archor/Goblin_Archor", new Vector3(7f,7f,0f));
        Item item = itemDataManager.ItemGen(10003001);
        player.GetComponent<Player>().Equip(0, item);
    }

    // Stage Scene Initialize
    public void stageSceneInit()
    {
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        cameraManager.camera = mainCamera;
        if(gameState == GameState.Loading)
        {
            gameStart();
        }
        gameState = GameState.InPlay;
        uiManager.ChangeState(UIState.InPlay);
        uiManager.stableUI.GetComponent<StableUI>().WeaponSlotChange(0);
    }

    // Tutorial Scene Initialize
    public void tutorialSceneInit() 
    {
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        cameraManager.camera = mainCamera;
        StageC.tutorial();
        playerGen();
        gameState = GameState.InPlay;
        uiManager.ChangeState(UIState.InPlay);
    }

    // Stage Scene Game Start
    public void gameStart()
    {
        mapgen();
        playerGen();
        Item item = itemDataManager.ItemGen(10003001);
        player.GetComponent<Player>().Equip(0, item);
        cameraManager.background = MapGen.bg.GetComponent<SpriteRenderer>();
        StageC.moveRoom(0);
       
    }

    public void gameOver()
    {
        // end
    }

    
    public void pauseGame()
    {
        {
            if (!isPaused)
            {
                Debug.Log("Pause");
                Time.timeScale = 0f; // Pause Game
                isPaused = true;
                GameObject go = Resource.Instantiate("UI/Pause", canvas.transform);
                go.name = "pause";
            }
            else
            {
                Debug.Log("Resume");
                GameObject go = GameObject.Find("pause");
                Resource.Destroy(go);
                Time.timeScale = 1f; // Resume Game
                isPaused = false;

            }
        }

    }

    public void playerDie()
    {

    }

    public void gameEnd()
    {

    }

    public void stageEnd()
    {

    }

   
    public static void mapgen()
    {
        clear();
        StageC.gameStart();
        cameraManager.background = MapGen.bg.GetComponent<SpriteRenderer>();
    }

    public static void clear()
    {
        StageC.clear();
        MapGen.clear();
    }
    // Player Gen game Start
    public void playerGen()
    {
        player = Resource.Instantiate("PlayerbleChara");
        player.transform.position = new Vector3(0, 0, 1);
        player.name = "PlayerbleChara";
        cameraManager.setCamera(player.transform);
        PlayerDataManager.interactionDistance = 1000.0f;

        uiManager.invUI.GetComponent<Inventory>().targetSet();
        uiManager.stableUI.GetComponent<StableUI>().init();
    }


    public void setCanvas()
    {
        Debug.Log("Set Canvas");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (canvas.gameObject.GetComponent<UIManager>() == null)
        {
            canvas.gameObject.AddComponent<UIManager>();
        }
        uiManager = canvas.GetComponent<UIManager>();
        uiManager.init();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas.planeDistance = 7;
    }

    // Item Drop Object Generate
    public void ItemDrop(int index, Transform position)
    {
        GameObject go = Resource.Instantiate("Obj/DropItem");
        go.GetComponent<DropItem>().item = itemDataManager.ItemGen(index);
        go.GetComponent<DropItem>().setSpr();
        go.transform.position = position.position;
    }

    // Get Text Datas
    public List<string> getTextData(int index)
    {
        return Script.getTxtData(index);
    }

}