using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopper : NPC
{
    public void Start()
    {
        interactDist = 2f;
        text = "��ȭ�Ѵ�";
        for(int i = 0; i < 6; i++)
        {
            Item itm = GameManager.itemDataManager.RandomItemGen();
            items.Add(itm);
            takeALook.Add(true);
        }
    }
}
