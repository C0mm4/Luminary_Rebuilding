using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [SerializeField]
    List<GameObject> selects = new List<GameObject>();
    [SerializeField]
    List<TMP_Text> texts;

    public override void Start()
    {
        base.Start();
        menusize = 2;
        currentMenu = 0;
        if(SceneManager.GetActiveScene().name != "LobbyScene")
        {
            texts[1].text = "Go To Lobby";
        }
    }

    public override void ConfirmAction()
    {
        selects[currentMenu].GetComponent<Choice>().Work();
    }


    public override void InputAction()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentMenu++;
            currentMenu %= menusize;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentMenu--;
            if(currentMenu < 0)
            {
                currentMenu = menusize - 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmAction();
        }
    }
    // Highlight select ui
    public void SelectHandler(int index)
    {
        selects[index].GetComponent<Image>().sprite = selects[index].GetComponent<Choice>().select;
        texts[index].color = Color.white;
    }
    // Highlight off select ui
    public void DeSelectHandler(int index)
    {
        selects[index].GetComponent<Image>().sprite = selects[index].GetComponent<Choice>().deSelect;
        texts[index].color = Color.gray;
    }

    public override void ESCInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.pauseGame();
            GameManager.Instance.uiManager.endMenu();
        }
    }
}
