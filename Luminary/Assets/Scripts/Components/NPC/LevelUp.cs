using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : Menu
{
    SerializedPlayerStatus playerStatus;
    SerializedPlayerStatus tmpStatus;
    public int totalSelect;
    public int strSelect;
    public int dexSelect;
    public int intSelect;

    public TMP_Text level;
    public TMP_Text reqGold;
    public TMP_Text currGold;
    public TMP_Text str;
    public TMP_Text dex;
    public TMP_Text intellect;
    public TMP_Text hp;
    public TMP_Text mp;
    public TMP_Text spd;
    public TMP_Text dmg;
    public TMP_Text strUp;
    public TMP_Text strDown;
    public TMP_Text dexUp;
    public TMP_Text dexDown;
    public TMP_Text intUp;
    public TMP_Text intDown;

    public List<Image> statusBg;

    public int requireGold;

    public override void ConfirmAction()
    {
        if(totalSelect != 0)
        {
            levelup();
        }
    }

    public override void InputAction()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if(currentMenu == -1)
            {
                closeButton.GetComponent<UICloseButton>().outHandler();
                currentMenu = 0;
                statusBg[currentMenu].color = new Color(0, 0, 0, 100f / 256f);
            }
            else
            {
                statusBg[currentMenu].color = new Color(1, 1, 1, 100f / 256f);
                currentMenu++;
                if (currentMenu >= menusize)
                {
                    currentMenu = 0;
                }
                statusBg[currentMenu].color = new Color(0, 0, 0, 100f / 256f);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if(currentMenu == -1)
            {
                closeButton.GetComponent<UICloseButton>().outHandler();
                currentMenu = 0;
                statusBg[currentMenu].color = new Color(0, 0, 0, 100f / 256f);
            }
            else
            {
                statusBg[currentMenu].color = new Color(1, 1, 1, 100f / 256f);
                currentMenu--;
                if (currentMenu < 0)
                {
                    currentMenu = menusize - 1;
                }
                statusBg[currentMenu].color = new Color(0, 0, 0, 100f / 256f);

            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if(currentMenu == -1)
            {
                closeButton.GetComponent<UICloseButton>().outHandler();
                currentMenu = 0;
                statusBg[currentMenu].color = new Color(0, 0, 0, 100f / 256f);
            }
            else
            {
                UpHandler();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if(totalSelect == 0)
            {
                statusBg[currentMenu].color = new Color(1, 1, 1, 100f / 256f);
                closeButton.GetComponent<UICloseButton>().inHandler();
            }
            DownHandler();
        }
        if (Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            if(currentMenu == -1)
            {
                GameManager.Instance.uiManager.endMenu();
            }
            else
            {
                levelup();
            }
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Func.SetRectTransform(gameObject);
        tmpStatus = new SerializedPlayerStatus();
        playerStatus = GameManager.player.GetComponent<Player>().status;
        tmpStatus = playerStatus;
        menusize = 3;
        requireGold = playerStatus.level * 1000;
        DataSet();
    }

    public void levelup()
    {
        playerStatus.level += totalSelect;
        playerStatus.strength += strSelect;
        playerStatus.dexterity += dexSelect;
        playerStatus.Intellect += intSelect;
        totalSelect = 0;
        strSelect = 0;
        dexSelect = 0;
        intSelect = 0;
        GameManager.player.GetComponent<Player>().status = playerStatus;
        int hp = playerStatus.maxHP;
        GameManager.player.GetComponent<Player>().calcStatus();
        Debug.Log(GameManager.player.GetComponent<Player>().status.maxHP - hp);
        GameManager.player.GetComponent<Player>().status.currentHP += GameManager.player.GetComponent<Player>().status.maxHP - hp;
        playerStatus.currentHP += GameManager.player.GetComponent<Player>().status.maxHP - hp;
        DataSet();
    }

    public void UpHandler()
    {
        totalSelect++;
        tmpStatus.gold -= requireGold;
        requireGold += (playerStatus.level + totalSelect) * 1000;
        tmpStatus.level++;
        switch (currentMenu)
        {
            case 0:
                strSelect++;
                tmpStatus.strength++;
                break;
            case 1:
                dexSelect++;
                tmpStatus.dexterity++;
                break;
            case 2:
                intSelect++;
                tmpStatus.Intellect++;
                break;
        }
        DataSet();
    }

    public void DownHandler()
    {
        switch (currentMenu)
        {
            case 0:
                if(strSelect > 0)
                {
                    tmpStatus.level--;
                    requireGold -= (playerStatus.level + totalSelect) * 1000;
                    tmpStatus.gold += requireGold;
                    totalSelect--;
                    strSelect--;
                    tmpStatus.strength--;
                }
                break;
            case 1:
                if (dexSelect > 0)
                {
                    tmpStatus.level--;
                    requireGold -= (playerStatus.level + totalSelect) * 1000;
                    tmpStatus.gold += requireGold;
                    totalSelect--;
                    dexSelect--;
                    tmpStatus.dexterity--;
                }
                break;
            case 2:
                if (intSelect > 0)
                {
                    tmpStatus.level--;
                    requireGold -= (playerStatus.level + totalSelect) * 1000;
                    tmpStatus.gold += requireGold;
                    totalSelect--;
                    intSelect--;
                    tmpStatus.Intellect--;
                }
                break;
        }
        DataSet();
    }

    public void DataSet()
    {
        tmpStatus = Func.calcStatus(tmpStatus);
        playerStatus = GameManager.player.GetComponent<Player>().status;
        level.text = playerStatus.level.ToString() + " -> " + (tmpStatus.level).ToString();
        reqGold.text = requireGold.ToString();
        currGold.text = playerStatus.gold.ToString() + " -> " + (tmpStatus.gold).ToString();
        str.text = playerStatus.strength.ToString() + " -> " + (tmpStatus.strength).ToString();
        dex.text = playerStatus.dexterity.ToString() + " -> " + (tmpStatus.dexterity).ToString();
        intellect.text = playerStatus.Intellect.ToString() + " -> " + (tmpStatus.Intellect).ToString();
        hp.text = playerStatus.maxHP.ToString() + " -> " + (tmpStatus.maxHP).ToString();
        mp.text = playerStatus.maxMana.ToString() + " -> " + (tmpStatus.maxMana).ToString();
        spd.text = playerStatus.speed.ToString() + " -> " + (tmpStatus.speed).ToString();
        dmg.text = playerStatus.finalDMG.ToString() + " -> " + (tmpStatus.finalDMG).ToString();
    }
}
 