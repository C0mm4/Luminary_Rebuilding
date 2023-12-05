using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : DunRoom
{
    [SerializeField]
    public GameObject boss;
    public override IEnumerator MobSpawn()
    {
        yield return new WaitForSeconds(1f);
        GameObject go = GameManager.mobSpawnner.bossSpawn(boss, spawnTrans[0]);
    }
}

