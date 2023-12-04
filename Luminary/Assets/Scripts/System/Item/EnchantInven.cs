using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnchantInven : BarInven
{

    [SerializeField]
    public GameObject targetName;
    public List<GameObject> statusText;
    public List<KeyValuePair<string, int>> status = new List<KeyValuePair<string, int>>();
    public string effectText;

    public override void InputAction()
    {
        // Move Current Menu with Arrow Key and wasd
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if(currentMenu == -1 || currentMenu == 99)
            {
                if (currentMenu == 99)
                {
                    confirmButton.GetComponent<ConfirmButton>().outHandler();
                }
                if(currentMenu == -1)
                {
                    closeButton.GetComponent<UICloseButton>().outHandler();
                }
                currentMenu = tmpIndex;
                slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
            }
            else
            {
                slots[currentMenu].GetComponent<ItemSlotBar>().outCursor();
                currentMenu++;
                if (currentMenu >= menusize)
                {
                    currentMenu = 0;
                }
                slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if(currentMenu == -1 || currentMenu == 99)
            {
                if(currentMenu == 99)
                {
                    confirmButton.GetComponent<ConfirmButton>().outHandler();
                }
                if(currentMenu == -1)
                {
                    closeButton.GetComponent<UICloseButton>().outHandler();
                }
                currentMenu = tmpIndex;
                slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
            }
            else
            {
                slots[currentMenu].GetComponent<ItemSlotBar>().outCursor();
                currentMenu--;
                if (currentMenu < 0)
                {
                    currentMenu = menusize - 1;
                }
                slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if(currentMenu != 99)
            {
                if(currentMenu == -1)
                {
                    closeButton.GetComponent<UICloseButton>().outHandler();
                    currentMenu = tmpIndex;
                    slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
                }
                else
                {
                    slots[currentMenu].GetComponent<ItemSlotBar>().outCursor();
                    tmpIndex = currentMenu;
                    confirmButton.GetComponent<ConfirmButton>().inHandler();
                    
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if(currentMenu != -1)
            {
                if(currentMenu == 99)
                {
                    closeButton.GetComponent<UICloseButton>().outHandler();
                    currentMenu = tmpIndex;
                    slots[currentMenu].GetComponent<ItemSlotBar>().onCursor();
                }
                else
                {
                    slots[currentMenu].GetComponent<ItemSlotBar>().outCursor();
                    tmpIndex = currentMenu;
                    closeButton.GetComponent<UICloseButton>().inHandler();
                }
            }
        }
        // If current Index is ItemSlot, ItemSlot Select
        if(Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            if (currentMenu < menusize && currentMenu != -1)
            {
                clickHandler(currentMenu);
            }
            // if current Index is close, close menu
            else if (currentMenu == -1)
            {
                GameManager.Instance.uiManager.endMenu();
            }
            // if current slot is Enchant Item, Enchant Table Menu Create
            else if (currentMenu == 99)
            {
                ConfirmAction();
            }
        }
        Debug.Log(currentMenu);
    }

    public override void show()
    {
        base.show();
        if(selectIndex != -1)
        {
            StatusUISet();
        }
    }

    public override void hide()
    {
        base.hide();
    }

    // When clicked Item Slots, past select slot run outCursor, new Slot set

    public override void clickHandler(int index)
    {
        int tmp = selectIndex;
        selectIndex = index;
        if(tmp >= 0 && tmp < menusize)
        {
            slots[tmp].GetComponent<ItemSlotBar>().outCursor();

        }
        slots[selectIndex].GetComponent<ItemSlotBar>().Select();

        // Set ConfirmButton Able
        confirmButton.GetComponent<ConfirmButton>().setAble();

        StatusUISet();

    }

    public override void hoverHandler(int index)
    {
        slots[index].GetComponent<ItemSlotBar>().onCursor();
        
    }

    public override void outHoverHandler(int index)
    {
        slots[index].GetComponent<ItemSlotBar>().outCursor();
    }

    public void StatusUISet()
    {
        // Item Status UI Data Set
        item = slots[selectIndex].GetComponent<ItemSlotBar>().Item;
        targetName.GetComponent<TMP_Text>().text = item.data.itemName;
        status = FindStatus(slots[selectIndex].GetComponent<ItemSlotBar>().Item);
        int i = 0;
        int last = -1;
        for (; i < statusText.Count; i++)
        {
            try
            {
                statusText[i].GetComponent<TMP_Text>().text = status[i].Key + " +" + status[i].Value;

            }
            catch
            {
                statusText[i].GetComponent<TMP_Text>().text = "";
                if (last == -1)
                {
                    last = i;
                }
            }
        }
        statusText[last].GetComponent<TMP_Text>().text = effectText;
    }

    public List<KeyValuePair<string, int>> FindStatus(Item itm)
    {
        // Find Item base Status - str, dex, int, hp, mp - and return
        List<KeyValuePair<string, int>> keyValuePairs = new List<KeyValuePair<string, int>>();
        if (itm.data.status.strength != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("STR", item.data.status.strength);
            keyValuePairs.Add(data);
        }
        if (itm.data.status.dex != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("DEX", item.data.status.dex);
            keyValuePairs.Add(data);
        }
        if (itm.data.status.intellect != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("INT", item.data.status.intellect);
            keyValuePairs.Add(data);
        }
        if (itm.data.status.increaseHP != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("MAX HP", item.data.status.increaseHP);
            keyValuePairs.Add(data);
        }
        if (itm.data.status.increaseMP != 0)
        {
            KeyValuePair<string, int> data = new KeyValuePair<string, int>("MAX MP", item.data.status.increaseMP);
            keyValuePairs.Add(data);
        }
        if (itm.data.effectText != "")
        {
            effectText = itm.data.effectText;
        }
        return keyValuePairs;
    }

    public override void ConfirmAction()
    {
        // if Item is Weapon > Weapon Enchant Table
        if(selectIndex != -1 && selectIndex != 99)
        {
            if (slots[selectIndex].GetComponent<ItemSlotBar>().Item.data.type == 0)
            {
                GameObject go = GameManager.Resource.Instantiate("UI/NPCUI/WeaponEnchant");
                go.GetComponent<EnchantTable>().targetItem = slots[selectIndex].GetComponent<ItemSlotBar>().Item;
                go.GetComponent<EnchantTable>().originSlotindex = slots[selectIndex].GetComponent<ItemSlotBar>().originSlot;
                if(slots[selectIndex].GetComponent<ItemSlotBar>().originSlot < 6)
                {
                    go.GetComponent<EnchantTable>().isEquip = true;
                }
                go.GetComponent<EnchantTable>().setData();
            }
            // if Item is not Weapon >> Item Enchant Table
            else
            {
                GameObject go = GameManager.Resource.Instantiate("UI/NPCUI/WeaponEnchant");
                go.GetComponent<EnchantTable>().targetItem = slots[selectIndex].GetComponent<ItemSlotBar>().Item;
                go.GetComponent<EnchantTable>().originSlotindex = slots[selectIndex].GetComponent<ItemSlotBar>().originSlot;
                if (slots[selectIndex].GetComponent<ItemSlotBar>().originSlot < 6)
                {
                    go.GetComponent<EnchantTable>().isEquip = true;
                    go.GetComponent<EnchantTable>().originSlotindex -= 2;
                }
                go.GetComponent<EnchantTable>().setData();
            }
        }
    }
}
