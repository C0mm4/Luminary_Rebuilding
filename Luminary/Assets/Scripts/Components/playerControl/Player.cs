using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Player : Charactor
{
    public float lastCastTime;
    public float lastManaGenTime;

    public SkillSlot[] skillslots;
    public List<SkillSlot> spells;
    public SkillSlot currentSpell;

    [SerializeField]
    InteractionTrriger interactionTrriger;

    public bool isInit = false;
    public bool ismove = false;
    [SerializeField]
    public Transform staffPos;

    public override void Awake()
    {
        base.Awake();
        player = this;
        GameManager.player = player.gameObject;

        // Set Skill Slots

        skillslots = new SkillSlot[3];
        spells = new List<SkillSlot>();
        setSkillSlots();


        // Get Status Data
        status = PlayerDataManager.playerStatus;
        calcStatus();
        // Add Die Handler
        GameManager.Instance.SceneChangeAction += DieObject;


        sMachine.changeState(new PlayerIdleState());
        isInit = true;
        currentSpell = skillslots[1];

    }
    private void setSkillSlots()
    {
        skillslots[0] = new SkillSlot();
        skillslots[1] = new SkillSlot();
        skillslots[2] = new SkillSlot();
    }

    
    public override void DieObject()
    {
        GameManager.inputManager.KeyAction -= spellKey;
        GameManager.Instance.SceneChangeAction -= DieObject;
        PlayerDataManager.playerStatus = status;
        base.DieObject();
    }

    public void OnDestroy()
    {
        DieObject();

    }

    public override void Update()
    {
        charactorSpeed = Vector2.zero;
        ManaGen();
        moveKey();
        if (getState() == null)
        {
            changeState(new PlayerIdleState());
        }

        // Interact with Interaction Objects
        if (Input.GetKeyDown(PlayerDataManager.keySetting.InteractionKey))
        {
            if (PlayerDataManager.interactionObject != null)
            {
                interactionTrriger = PlayerDataManager.interactionObject.GetComponent<InteractionTrriger>();
                interactionTrriger.isInteraction();
            }
        }

        // is charactor move set move state
        if (charactorSpeed != Vector2.zero)
        {
            ismove = true;
            changeState(new PlayerMoveState());
        }
        base.Update();

    }

    // didn't cast 3 seconds later mana refill
    public void ManaGen()
    {
        if(Time.time - lastCastTime >= 3f)
        {
            if(Time.time - lastManaGenTime >= 0.2f)
            {
                status.currentMana++;
                if(status.currentMana >= status.maxMana)
                {
                    status.currentMana = status.maxMana;
                }
            }
        }
    }

    public void moveKey()
    {
        if (GameManager.uiState == UIState.InPlay || GameManager.uiState == UIState.Lobby)
        {
            // Teleport some Distance
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (skillslots[0].isSet())
                {
                    if (ismove)
                        StartCoroutine("roll");
                }
            }
            // charactor move vector set with WASD
            if (getState().GetType().Name == "PlayerIdleState" || getState().GetType().Name == "PlayerMoveState")
            {
                if (Input.GetKey(KeyCode.W))
                {
                    charactorSpeed.y = status.speed;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    charactorSpeed.y = -status.speed;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    charactorSpeed.x = -status.speed;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    charactorSpeed.x = status.speed;
                }

            }
        }
    }

    public void spellKey()
    {
        if(GameManager.uiState == UIState.InPlay || GameManager.uiState == UIState.Lobby)
        {
            // Mouse left click casting spells
            if (Input.GetMouseButtonDown(0))
            {
                if (currentSpell.isSet())
                {
                    currentSpell.useSkill();
                    Debug.Log("attackSound.Play");
                    GameManager.soundManager.playAttackSound();
                }
            }
            // Q button is Weapon Slot Change
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (currentSpell == skillslots[1])
                {
                    Debug.Log("SpellSlot Change 2");
                    currentSpell = skillslots[2];
                    GameManager.Instance.uiManager.stableUI.GetComponent<StableUI>().WeaponSlotChange(0);
                }
                else
                {
                    Debug.Log("SpellSlot Change 1");
                    currentSpell = skillslots[1];
                    GameManager.Instance.uiManager.stableUI.GetComponent<StableUI>().WeaponSlotChange(1);
                }
            }
        }

    }


    public IEnumerator roll()
    {
        if(GameManager.uiState == UIState.InPlay || GameManager.uiState == UIState.Lobby)
        {
            if (GameManager.FSM.getList(sMachine.getStateStr()).Contains("PlayerCastingState"))
            {
                if (skillslots[0].isSet())
                {
                    Debug.Log(charactorSpeed);
                    changeState(new PlayerCastingState(skillslots[0].getSpell(), charactorSpeed));
                    skillslots[0].useSkill();
                }

                yield return new WaitForSeconds(0);
            }

        }

    }
    // Item Equip in inventory[index] to equip[targetslotIndex]
    public void ItemEquip(int index, Item item, int targetslotindex = -1)
    {
        if (currentequipSize < 4)
        {
            // if targetslotIndex is -1, last slot equips
            if (targetslotindex == -1)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (status.equips[i].item == null)
                    {
                        status.equips[i].AddItem(item);
                        status.inventory[index].RemoveItem();
                        break;
                    }
                }
            }
            else
            {
                // if target slot is already exists items, swap items
                if (status.equips[targetslotindex].item != null)
                {
                    Item tmp = status.equips[targetslotindex].item;
                    status.equips[targetslotindex].RemoveItem();
                    status.equips[targetslotindex].AddItem(status.inventory[index].item);
                    status.inventory[index].RemoveItem();
                    ItemAdd(tmp, index);
                }
                else
                {
                    status.equips[targetslotindex].AddItem(item);
                    status.inventory[index].RemoveItem();
                }
            }
            GameManager.Instance.uiManager.invenFresh();
            calcStatus();
        }
        else
        {
            Debug.Log("Full Equiped");
        }
    }
    // Item Equip in inventory[index] to weapons[targetslotIndex]
    public void WeaponEquip(int index, Item item, int targetslotindex = -1)
    {
        if (currentweaponSize < 2)
        {
            // if targetslotIndex is -1, last slot equips
            if (targetslotindex == -1)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (status.weapons[i].item == null)
                    {
                        Debug.Log(i + " slot equip");
                        status.weapons[i].AddItem(item);
                        skillslots[i + 1].setCommand(GameManager.Spells.spells[item.data.spellnum]);
                        GameManager.Instance.uiManager.stableUI.GetComponent<StableUI>().weaponSlot[i].GetComponent<WeaponSlotUI>().setWeapon(item);
                        break;
                    }
                }
            }
            else
            {
                if (status.weapons[targetslotindex].item == null)
                {
                    status.weapons[targetslotindex].AddItem(item);
                    skillslots[targetslotindex + 1].setCommand(GameManager.Spells.spells[item.data.spellnum]);
                    GameManager.Instance.uiManager.stableUI.GetComponent<StableUI>().weaponSlot[targetslotindex].GetComponent<WeaponSlotUI>().setWeapon(item);
                    status.inventory[index].RemoveItem();

                }
                else
                {               
                    // if target slot is already exists items, swap items
                    Item tmp = status.weapons[targetslotindex].item;
                    status.weapons[targetslotindex].RemoveItem();
                    status.weapons[targetslotindex].AddItem(status.inventory[index].item);
                    skillslots[targetslotindex + 1].setCommand(GameManager.Spells.spells[item.data.spellnum]);
                    GameManager.Instance.uiManager.stableUI.GetComponent<StableUI>().weaponSlot[targetslotindex].GetComponent<WeaponSlotUI>().setWeapon(item);
                    status.inventory[index].RemoveItem();
                    ItemAdd(tmp, index);
                }

            }
            status.inventory[index].RemoveItem();
            GameManager.Instance.uiManager.invenFresh();
            calcStatus();
        }
        else
        {
            Debug.Log("Full Equiped");
        }
    }
    // Item unequip weapon[n] to inventory[targetslotindex]
    public void ItemUnequip(int n, int targetslotindex)
    {
        Item item = status.equips[n].item;
        // targetslotindex is -1, Item Add last unfill index
        if (targetslotindex == -1)
        {
            if (ItemAdd(status.equips[n].item))
            {
                status.equips[n].RemoveItem();
                GameManager.Instance.uiManager.invenFresh();
            }
            else
            {
                Debug.Log("Inventory is Full");
            }

        }
        else
        {
            if (status.inventory[targetslotindex].item == null)
            {
                ItemAdd(status.equips[n].item, targetslotindex);
                status.equips[n].RemoveItem();
            }
            else
            {
                // if invenrory[targetslotindex] has items, equip items and unequip itemss
                Item tmp = status.inventory[targetslotindex].item;
                if(tmp.data.type != 0)
                {
                    ItemAdd(status.equips[n].item, targetslotindex);
                    status.equips[n].RemoveItem();
                    status.equips[n].AddItem(tmp);
                }
            }
        }
        calcStatus();
    }

    // Same algorithm on ItemEquip
    public void WeaponUnequip(int n, int targetslotindex)
    {
        Item item = status.weapons[n].item;
        if(targetslotindex == -1)
        {
            if (ItemAdd(status.weapons[n].item))
            {
                status.weapons[n].RemoveItem();
//                spells[n+1].deSetCommand();
                GameManager.Instance.uiManager.invenFresh();
            }
            else
            {
                Debug.Log("Inventory is Full");
            }

        }
        else
        {
            if (status.inventory[targetslotindex].item == null)
            {
                ItemAdd(status.weapons[n].item, targetslotindex);
                status.weapons[n].RemoveItem();
                spells[n + 1].deSetCommand();
            }
            else
            {
                Item tmp = status.inventory[targetslotindex].item;
                if(tmp.data.type == 0)
                {
                    ItemAdd(status.weapons[n].item, targetslotindex);
                    status.weapons[n].RemoveItem();
                    status.weapons[n].AddItem(tmp);
                    spells[n + 1].deSetCommand();
                }
                
            }
        }
        
        calcStatus();
    }

    // Other class access this Equip Function. 
    public void Equip(int targetindex, Item item, int targetslotindex = -1)
    {
        if (item.data.type == 0)
        {
            WeaponEquip(targetindex, item, targetslotindex);
        }
        else
        {
            ItemEquip(targetindex, item, targetslotindex);
        }


        GameManager.Instance.uiManager.invenFresh();
    }
    // Other class access this Unequip function.
    public void Unequip(int index, Item item, int targetslotindex = -1)
    {
        if (item.data.type == 0)
        {
            WeaponUnequip(index, targetslotindex);
        }
        else
        {
            ItemUnequip(index, targetslotindex);
        }
        GameManager.Instance.uiManager.invenFresh();
    }

    // swap items in inventory
    public void ItemSwap(int sindex, int tindex)
    {
        Item tmp = status.inventory[sindex].item;
        status.inventory[sindex].item = status.inventory[tindex].item;
        status.inventory[tindex].item = tmp;

        GameManager.Instance.uiManager.invenFresh();
    }

    // swap items in equip slots
    public void EquipSwap(int sindex, int tindex)
    {
        Item tmp = status.equips[sindex].item;
        status.equips[sindex].item = status.equips[tindex].item;
        status.equips[tindex].item = tmp;

        GameManager.Instance.uiManager.invenFresh();
    }

    // swap items in weapon equip slots
    public void WeaponSwap(int sindex, int tindex)
    {
        Item tmp = status.equips[sindex].item;
        status.weapons[sindex].item = status.weapons[tindex].item;
        status.weapons[tindex].item= tmp;

        GameManager.Instance.uiManager.invenFresh();
    }
}
