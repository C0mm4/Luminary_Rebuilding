using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using UnityEngine.UI;

public class Inventory : Menu
{
    [SerializeField]
    private ItemSlot[] slots;

    [SerializeField]
    private ItemSlot[] equips;

    [SerializeField]
    private ItemSlot[] equipWeapons;

    [SerializeField]
    private Transform bag;

    [SerializeField]
    private Transform equip;

    RectTransform rt;


    [SerializeField]
    private GameObject target;

    public GameObject tmpitem;

    public GameObject hoveringUI;

    public TMP_Text hp;
    public TMP_Text mp;
    public TMP_Text str;
    public TMP_Text dex;
    public TMP_Text intellect;

    public Image spellSlot1;
    public Image spellSlot2;

    public int clickIndex = -1;

    public override void Start()
    {

    }


    private void OnValidate()
    {
        slots = bag.GetComponentsInChildren<ItemSlot>();
        equips = equip.GetComponentsInChildren<ItemSlot>();
        GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public override void show()
    {
        freshSlot();
        base.show();
    }

    public override void exit()
    {
        try
        {
            GameManager.Resource.Destroy(hoveringUI);

        }
        catch { }
        hide();
    }



    public void init()
    {
        Func.SetRectTransform(this.gameObject);
        target = GameManager.player;
        gameObject.SetActive(false);
        // Test
    }

    public void targetSet()
    {
        target = GameManager.player;
        Debug.Log(target);
    }
    
    private void Awake()
    {
        freshSlot();
    }

    public void freshSlot()
    {
        if (target != null)
        {
            for (int i = 0; i < slots.Length && i < target.GetComponent<Charactor>().status.inventory.Count; i++)
            {
                slots[i].item = target.GetComponent<Charactor>().status.inventory[i].item;
            }
            for (int i = 0; i < equips.Length && i < target.GetComponent<Charactor>().status.equips.Count; i++)
            {
                equips[i].item = target.GetComponent<Charactor>().status.equips[i].item;
            }
            for (int i = 0; i < equipWeapons.Length && i < target.GetComponent<Charactor>().status.weapons.Count; i++)
            {
                equipWeapons[i].item = target.GetComponent<Charactor>().status.weapons[i].item;
            }
            hp.text = target.GetComponent<Player>().status.currentHP + " / " + target.GetComponent<Player>().status.maxHP;
            mp.text = target.GetComponent<Player>().status.currentMana + " / " + target.GetComponent<Player>().status.maxMana;
            str.text = target.GetComponent<Player>().status.strength.ToString();
            dex.text = target.GetComponent<Player>().status.dexterity.ToString();
            intellect.text = target.GetComponent<Player>().status.Intellect.ToString();
            if (equipWeapons[0].item != null)
            {
                spellSlot1.sprite = GameManager.Spells.getSpellData(equipWeapons[0].item.data.spellnum).spr;
            }
            else
            {
                spellSlot1.sprite = null;
            }
            if (equipWeapons[1].item != null)
            {
                spellSlot2.sprite = GameManager.Spells.getSpellData(equipWeapons[1].item.data.spellnum).spr;
            }
            else
            {
                spellSlot2.sprite = null;
            }
        }
    }

    public override void InputAction()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            Debug.Log("I Key Input");
            GameManager.Instance.uiManager.endMenu();
        }
    }

    public override void ConfirmAction()
    {
    }
}
