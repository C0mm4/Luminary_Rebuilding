using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : DunRoom
{
    [SerializeField]
    public GameObject boss;
    public override IEnumerator MobSpawn()
    {
        yield return 0;
        GameObject go = GameManager.mobSpawnner.bossSpawn(boss, spawnTrans[0]);
    }

    public override void OpenDoor()
    {
        base.OpenDoor();
    }
}

