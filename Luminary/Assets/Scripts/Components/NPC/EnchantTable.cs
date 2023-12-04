using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnchantTable : Menu
{
    public Item targetItem;
    public int originSlotindex;
    public bool isEquip;

    [SerializeField]
    public Image itemImg;
    public TMP_Text itemName;
    public TMP_Text level;
    public TMP_Text reqGold;
    public TMP_Text curGold;
    public TMP_Text str;
    public TMP_Text dex;
    public TMP_Text intelect;
    public TMP_Text HP;
    public TMP_Text MP;

    public GameObject confirmButton;

    public override void Start()
    {
        base.Start();
        Func.SetRectTransform(gameObject);
    }

    // Set Status Increase Data
    public void setData()
    {
        itemImg.sprite = targetItem.data.itemImage;
        itemName.text = targetItem.data.itemName;
        level.text = targetItem.data.level.ToString() + " - > " + (targetItem.data.level + 1).ToString();
        reqGold.text = (targetItem.data.increaseStatus.baseGold + targetItem.data.level * targetItem.data.increaseStatus.increaseGold).ToString() + " G";
        curGold.text = (GameManager.player.GetComponent<Player>().status.gold).ToString() + " G";
        str.text = (targetItem.data.status.strength).ToString() + " - > " + (targetItem.data.status.strength + targetItem.data.increaseStatus.strength).ToString();
        dex.text = (targetItem.data.status.dex).ToString() + " - > " + (targetItem.data.status.dex + targetItem.data.increaseStatus.dex).ToString();
        intelect.text = (targetItem.data.status.intellect).ToString() + " - > " + (targetItem.data.status.intellect + targetItem.data.increaseStatus.intellect).ToString();
        HP.text = (targetItem.data.status.increaseHP).ToString() + " - > " + (targetItem.data.status.increaseHP + targetItem.data.increaseStatus.increaseHP).ToString();
        MP.text = (targetItem.data.status.increaseMP).ToString() + " - > " + (targetItem.data.status.increaseMP + targetItem.data.increaseStatus.increaseMP).ToString();

        if(targetItem.data.increaseStatus.baseGold + targetItem.data.level * targetItem.data.increaseStatus.increaseGold <= GameManager.player.GetComponent<Player>().status.gold)
        {
            confirmButton.GetComponent<ConfirmButton>().isAble = true;
        }
        else
        {
            confirmButton.GetComponent<ConfirmButton>().isAble = true;
        }
    }

    public override void ConfirmAction()
    {
        // Enchant Items
        if (confirmButton.GetComponent<ConfirmButton>().isAble)
        {
            LevelUp();

        }
    }

    public override void InputAction()
    {
        if (Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            ConfirmAction();
        }
    }

    public void LevelUp()
    {
        if (isEquip)
        {
            if (targetItem.data.type == 0)
            {
                Debug.Log(GameManager.player.GetComponent<Player>().status.strength);
                GameManager.player.GetComponent<Player>().status.weapons[originSlotindex].RemoveItem();
            }
        }
        GameManager.player.GetComponent<Player>().calcStatus();
        Debug.Log(GameManager.player.GetComponent<Player>().status.strength);
        targetItem.data.StatusUpgrade();
        if (isEquip)
        {
            if (targetItem.data.type == 0)
            {
                Debug.Log(targetItem.data.status.strength);
                GameManager.player.GetComponent<Player>().status.weapons[originSlotindex].AddItem(targetItem);
            }
        }

        Debug.Log(GameManager.player.GetComponent<Player>().status.strength);
        GameManager.player.GetComponent<Player>().calcStatus();
        GameManager.player.GetComponent<Player>().status.gold -= targetItem.data.increaseStatus.baseGold + targetItem.data.level * targetItem.data.increaseStatus.increaseGold;
        Debug.Log(GameManager.player.GetComponent<Player>().status.strength);
        setData();
    }
}
