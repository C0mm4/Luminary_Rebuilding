using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Camera camera;
    bool binvenUI = false, bskillUI = false, bskillSlotUI = false, binGameUI = false, bbossUI = false, bmapUI = false;

    [SerializeField]
    public GameObject invUI;
    public GameObject stableUI;
    public GameObject menuUI;
    public GameObject pauseUI;
    public GameObject loadUI;

    public bool isInit = false;


    Queue<string> textUIqueue = new Queue<string>();
    private float textUItime = -3f;

    public Menu currentMenu = null;
    public Stack<Menu> menuStack = new Stack<Menu>();

    Stack<UIState> uistack = new Stack<UIState>();

    // Generate Menu Object, add stacks and hide before menu and showing new menu
    public void addMenu(Menu menu)
    {
        if(currentMenu != null)
        {
            currentMenu.hide();
            menuStack.Push(currentMenu);
        }
        currentMenu = menu;
        ChangeStateOnStack(UIState.Menu);
        currentMenu.show();
    }

    // Menu object closed, reset before menu
    public void endMenu()
    {
        Debug.Log("EndMenu");
        currentMenu.exit();
        currentMenu = null;
        if (menuStack.Count > 0)
        {
            currentMenu = menuStack.Pop();
            currentMenu.show();
        }
        PopStateStack();
    }

    // In Game UI State, UI Objects initialize
    public void init()
    {
        if(GameManager.uiState == UIState.Title)
        {
            ChangeState(UIState.Title);
        }
        // Find Camera and Canvas Objects
        if (camera == null)
        {
            camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        }
        if (canvas == null)
        {
            if (GameManager.Instance.canvas != null)
            {
                canvas = GameManager.Instance.canvas;
            }
        }
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            
        }

        if (GameManager.uiState != UIState.Title)
        {
            UIClear();
        }
        invUI = GameManager.Resource.Instantiate("UI/Inventory2");
        invUI.GetComponent<Inventory>().init();
        

        textUItime = -3f;
        isInit = true;


        stableUI = GameManager.Resource.Instantiate("UI/StableUI");
        Func.SetRectTransform(stableUI);
        stableUI.SetActive(false);
    }

    // Destroy all UI Object
    public void UIClear()
    {
        foreach(Transform child in transform)
        {
            GameManager.Resource.Destroy(child.gameObject);
        }
    }

    // Inventory item slots refreshing
    public void invenFresh()
    {
        invUI.GetComponent<Inventory>().freshSlot();
    }

    // Added Text Queue showing text
    public void textUI(string txt)
    {
        textUIqueue.Enqueue(txt);
    }


    private void GenTextUI()
    {
        var obj = GameManager.Resource.Instantiate("UI/TextUI");
        obj.GetComponent<TextUI>().text = textUIqueue.Dequeue();
    }

    public void InPlayInput()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Tap Interaction
        }
        if(Input.GetKeyDown(PlayerDataManager.keySetting.inventoryKey))
        {
            if(GameManager.uiState != UIState.Menu)
            {
                addMenu(invUI.GetComponent<Menu>());
            }
        }
    }


    public void InventoryToggleInput()
    {
        if (Input.GetKeyDown(PlayerDataManager.keySetting.inventoryKey))
        {
            if (GameManager.uiState != UIState.Menu) 
            {
                addMenu(invUI.GetComponent<Menu>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameState != GameState.Loading)
        {
            if (!isInit)
            {
                if (GameManager.Instance.uiManager != null)
                {
                    init();
                }
            }
            // Draw TEXT UI
            if (textUIqueue.Count > 0)
            {
                if (Time.time - textUItime > 2.5f)
                {
                    textUItime = Time.time;
                    GenTextUI();
                }
            }

            if (invUI == null)
            {
                try
                {
                    invUI = canvas.transform.Find("Inventory2(Clone)").gameObject;

                }
                catch { }
            }
            if (GameManager.uiState == UIState.InPlay)
            {
                if(stableUI != null)
                    stableUI.SetActive(true);
            }
            else
            {
                if(stableUI != null)
                    stableUI.SetActive(false);
            }
        }
    }
    // Change UI State
    public void ChangeState(UIState state)
    {
        
        GameManager.uiState = state;

        GameManager.inputManager.changeInputState();

    }
    // Change Ui State on Stack (Used Menus)
    public void ChangeStateOnStack(UIState state)
    {
        uistack.Push(GameManager.uiState);
        Debug.Log("UIStack Push");
        ChangeState(state);
    }

    // When Menu Close Stack UI Change
    public void PopStateStack()
    {
        Debug.Log(uistack.Count);
        ChangeState(uistack.Pop());
    }
}
