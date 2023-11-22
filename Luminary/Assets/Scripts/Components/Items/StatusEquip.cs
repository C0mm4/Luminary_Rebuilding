﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEquip : ItemFunc
{

    public override void EquipEffect()
    {
        Debug.Log("TEST");
        GameManager.player.GetComponent<Player>().ItemStatusSum(data.status);
        Debug.Log(data.status.intellect);
    }

    public override void OnDamagedEffect()
    {
    }

    public override void OnFrameEffect()
    {
    }

    public override void OnHitEffect()
    {
    }

    public override void UnEquipEffect()
    {
        Debug.Log("TestItem1 UnEquip");
        GameManager.player.GetComponent<Player>().ItemStatusminus(data.status);
    }
}
