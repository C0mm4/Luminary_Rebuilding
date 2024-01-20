using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOnHitItem : ItemFunc
{

    public override void EquipEffect()
    {
        Debug.Log("TEST");
        GameManager.player.GetComponent<Player>().ItemStatusSum(data.status);
        Debug.Log(data.status.strength);
        GameManager.player.GetComponent<Player>().attackEffect += OnHitEffect;
    }

    public override void OnDamagedEffect()
    {
    }

    public override void OnFrameEffect()
    {
    }

    public override void OnHitEffect(GameObject spellObj)
    {

    }

    public override void UnEquipEffect()
    {
        GameManager.player.GetComponent<Player>().ItemStatusminus(data.status);
    }
}
