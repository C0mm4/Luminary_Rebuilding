using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action KeyAction = null;
    public Vector3 mousePos = new Vector3(), mouseWorldPos = new Vector3();
    public float MenuCloseT;
    private string screenshotFolder = "Screenshots";
    public bool isDragging;
    // Key Input Event Check
    public void OnUpdate()
    {
        if(KeyAction != null)
            KeyAction();
           
    }

    // Check Mouse position and key inputs
    public void Update()
    {
        OnUpdate();
        mousePos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("G");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.time - MenuCloseT >= 1f)
            {
                if (GameManager.gameState == GameState.Pause)
                {
                    GameManager.Instance.pauseGame();
                    GameManager.Instance.uiManager.endMenu();
                }
                else if ((GameManager.gameState != GameState.Loading)
                    && (GameManager.uiState == UIState.InPlay || GameManager.uiState == UIState.Lobby))
                {
                    GameManager.Instance.pauseGame();
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            // 스크린샷 찍기
            CaptureScreenshot();
        }
    }
    void CaptureScreenshot()
    {
        // Screenshot 폴더 경로 생성
        string folderPath = Path.Combine(Application.dataPath, screenshotFolder);

        // Screenshot 폴더가 없으면 생성
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // 현재 날짜와 시간을 이용하여 파일명 생성
        string screenshotName = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        // 스크린샷 찍기
        string filePath = Path.Combine(folderPath, screenshotName);
        ScreenCapture.CaptureScreenshot(filePath);

        // 콘솔에 메시지 출력
        Debug.Log("Screenshot captured: " + screenshotName);
        Debug.Log("Saved to: " + filePath);
    }
    // Change Input Events by InGame UI States
    public void changeInputState()
    {
        KeyAction = null;
        switch (GameManager.uiState)
        {
            case UIState.Title:
                KeyAction += TitleInput;
                break;
            case UIState.Lobby:
                LobbyInput();
                break;
            case UIState.InPlay:
                InGameInput(); 
                break;
            case UIState.Inventory:
                break;
            case UIState.Menu:

                break;
            case UIState.Setting:

                break;
            case UIState.CutScene:
                
                break;
            case UIState.Pause:
                break;
        }
    }
    
    // States Input Set
    public void TitleInput()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            GameManager.Instance.uiManager.ChangeState(UIState.Loading);
            GameManager.Instance.sceneControl("LobbyScene");
        }
    }

    public void LobbyInput()
    {
        KeyAction += GameManager.Instance.uiManager.InventoryToggleInput;
        KeyAction += GameManager.player.GetComponent<Player>().spellKey;
    }

    public void InGameInput()
    {
        KeyAction += GameManager.Instance.uiManager.InPlayInput;
        KeyAction += GameManager.player.GetComponent<Player>().spellKey;
    }
}
