using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{

    [SerializeField]
    public List<ItemData> data = new List<ItemData>();
    public List<ItemData> enchantData = new List<ItemData>();

    public Dictionary<int, ItemData> itemDictionary = new Dictionary<int, ItemData>();

    public List<SerializeEnchantData> enchantDictionary = new List<SerializeEnchantData>();

    public int commonN, rareN, uniqueN, epicN;
    public int staffcN, staffrN, staffuN, staffeN;

    public void Init()
    {
        // Search Item Data and Dictionary Initialize
        // Count Rarity Items
        commonN = rareN = uniqueN = epicN = 0;
        foreach(ItemData item in data)
        {
            if(item != null)
            {
                // Check Item Rarity by Index
                itemDictionary[item.itemIndex] = item;
                int rarity = item.itemIndex / 100;
                switch (rarity)
                {
                    case 100020:
                        commonN++;
                        break;
                    case 100021:
                        rareN++;
                        break;
                    case 100022:
                        uniqueN++;
                        break;
                    case 100023:
                        epicN++;
                        break;
                    case 100025:
                        staffcN++;
                        break;
                    case 100026:
                        staffrN++;
                        break;
                    case 100027:
                        staffuN++;
                        break;
                    case 100028:
                        staffeN++;
                        break;
                }
            }
        }
    }
    // Return itemIndex data
    public ItemData getItemData(int itemIndex)
    {
        ItemData data = ScriptableObject.CreateInstance<ItemData>();
        data.Initialize(itemDictionary[itemIndex]);
        Debug.Log(data.sellGold);
        return data;
    }

    // Genrate Item
    public Item ItemGen(int itemindex)
    {
        Item item = new Item();
        item.data = getItemData(itemindex);
        Type T = Type.GetType(item.data.funcName);
        ItemFunc func = Activator.CreateInstance(T) as ItemFunc;
        item.data.func = func;
        item.data.func.data = item.data;
        item.initCalc();

        return item;
    }

    // Generate Random Item
    public Item RandomItemGen()
    {
        int index = 100020;
        int isStaff = GameManager.Random.getShopNext();
        int rnd = GameManager.Random.getShopNext(); 
        int rarity = 0;
        if (isStaff < 5)
        {
            if (rnd < 60)
            {
                rarity = 5;
            }
            else if (rnd < 100) // 85
            {
                rarity = 6;
            }
/*
            else if (rnd < 95)
            {
                rarity = 7;
            }
            else
            {
                rarity = 8;
            }
*/
        }
        else
        {

            if (rnd < 70)   // 40
            {
                rarity = 0;
            }
            else if (rnd < 100) // 70
            {
                rarity = 1;
            }
/*
            else if (rnd < 90)
            {
                rarity = 2;
            }
            else
            {
                rarity = 3;
            }*/
        }
        
        index += rarity;
        index *= 100;
        int specificIndex;
        switch (rarity)
        {
            case 0:
                specificIndex = GameManager.Random.getShopNext(1, commonN + 1);
                index += specificIndex;
                break;
            case 1:
                specificIndex = GameManager.Random.getShopNext(1, rareN + 1);
                index += specificIndex;
                break;
            case 2:
                specificIndex = GameManager.Random.getShopNext(1, uniqueN + 1);
                index += specificIndex;
                break;
            case 3:
                specificIndex = GameManager.Random.getShopNext(1, epicN + 1);
                index += specificIndex;
                break;
            case 5:
                specificIndex = GameManager.Random.getShopNext(1, staffcN + 1);
                index += specificIndex;
                break;
            case 6:
                specificIndex = GameManager.Random.getShopNext(1, staffrN + 1);
                index += specificIndex;
                break;
            case 7:
                specificIndex = GameManager.Random.getShopNext(1, staffuN + 1);
                index += specificIndex;
                break;
            case 8:
                specificIndex = GameManager.Random.getShopNext(1, staffeN + 1);
                index += specificIndex;
                break;
        }
        Item item = ItemGen(index);


        return item;
    }

    public (Item, Item) BaseStaff()
    {
        int index = 10002500;
        Item item = ItemGen(index + GameManager.Random.getShopNext(1, staffcN + 1));
        Item item2 = ItemGen(index + GameManager.Random.getShopNext(1, staffcN + 1));
        // return defferent weapons
        while(item2.data.itemIndex == item.data.itemIndex)
        {
            item2 = ItemGen(index + GameManager.Random.getShopNext(1, staffcN + 1));
        }
        return (item, item2);
    }
}
