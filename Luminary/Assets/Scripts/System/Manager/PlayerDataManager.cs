using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    // Ingame Key Settings
    public static SerializedKeySetting keySetting = new SerializedKeySetting();
    // Player Status Data
    public static SerializedPlayerStatus playerStatus = new SerializedPlayerStatus();

    // Interaction Object
    public static GameObject interactionObject = null;
    public static float interactionDistance;
    public static bool isInteractObjDetect = false;
    public static GameObject interactUI;


    public struct SerializedKeySetting
    {
        public KeyCode inventoryKey;
        public KeyCode InteractionKey;
    }
    // Set Player Initialize Data
    public void playerDataInit()
    {
        playerStatus.dexterity = 1;
        playerStatus.strength = 1;
        playerStatus.Intellect = 1;

        playerStatus.baseDMG = 1;
        playerStatus.increaseDMG = 0;
        playerStatus.pIncreaseDMG = 0;

        playerStatus.igniteDMG = 0;
        playerStatus.freezeDMG = 0;
        playerStatus.flowDMG = 0;
        playerStatus.shockDMG = 0;
        playerStatus.electDMG = 0;
        playerStatus.seedDMG = 0;

        playerStatus.meltingDMG = 0;
        playerStatus.extinguishDMG = 0;
        playerStatus.fireDMG = 0;
        playerStatus.electFireDMG = 0;
        playerStatus.burnningDMG = 0;
        playerStatus.crackedDMG = 0;
        playerStatus.rootedDMG = 0;
        playerStatus.electShockDMG = 0;
        playerStatus.expandDMG = 0;
        playerStatus.sproutDMG = 0;
        playerStatus.dischargeDMG = 0;
        playerStatus.weatheringDMG = 0;
        playerStatus.boostDMG = 0;
        playerStatus.diffusionDMG = 0;
        playerStatus.overloadDMG = 0;
        playerStatus.executionDMG = 0;
        
        playerStatus.def = 0;

        playerStatus.baseHP = 5;
        playerStatus.increseMaxHP = 0;
        playerStatus.pIncreaseMaxHP = 0;
        playerStatus.maxHP = (int)Mathf.Floor((playerStatus.baseHP + playerStatus.increseMaxHP) * (1+playerStatus.pIncreaseMaxHP));
        playerStatus.currentHP = playerStatus.maxHP;

        playerStatus.baseMana = 10;
        playerStatus.increaseMaxMana = 0;
        playerStatus.pIncreaseMaxMana = 0;
        playerStatus.maxMana = (int)Mathf.Floor((playerStatus.baseMana + playerStatus.increaseMaxMana) * (1+playerStatus.pIncreaseMaxMana));
        playerStatus.currentMana = playerStatus.maxMana;

        playerStatus.basespeed = 5;
        playerStatus.increaseSpeed = 0;
        playerStatus.pIncreaseSpeed = 0;


        playerStatus.pGetDMG = 1;
        playerStatus.level = 1;
        playerStatus.gold = 0;
        playerStatus.element = new ElementData();
        playerStatus.inventory = new List<ItemSlotChara>();
        playerStatus.equips = new List<EquipSlotChara>();
        playerStatus.weapons = new List<WeaponSlotChara>();
        playerStatus.buffs = new List<Buff>();
        playerStatus.endbuffs = new List<Buff>();

        for(int i = 0; i < 12; i++)
        {
            playerStatus.inventory.Add(new ItemSlotChara());
        }
        for(int i = 0; i < 4; i++)
        {
            playerStatus.equips.Add(new EquipSlotChara());
        }
        for(int i = 0; i < 2; i++)
        {
            playerStatus.weapons.Add(new WeaponSlotChara());
        }

    }
    // Loading key Settings
    public void loadKeySetting()
    {
        keySetting.inventoryKey = (KeyCode)PlayerPrefs.GetInt("inventoryKey", (int)KeyCode.I);
        keySetting.InteractionKey = (KeyCode)PlayerPrefs.GetInt("InteractionKey", (int)KeyCode.F);
    }
    // Save Key Settings
    public void saveKeySetting()
    {
        PlayerPrefs.SetInt("inventoryKey", (int)keySetting.inventoryKey);
        PlayerPrefs.SetInt("InteractionKey", (int)keySetting.InteractionKey);
    }

    public void savePlayerData()
    {

    }

}
