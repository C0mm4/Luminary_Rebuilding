using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execution : Buff
{
    public Execution(Charactor tar, Charactor atk, int stackCnt, int dmg) : base(tar, atk)
    {
        id = 25;
        setDurate(0);
        setTickTime(0);

        cooltime = 5f;

        this.dmg = (int)Mathf.Floor(0.1f * stackCnt * dmg) * (100 + tar.GetComponent<Player>().status.executionDMG) / 100;
        Debug.Log(stackCnt);
        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.execution)
        {
            base.startEffect();
            target.HPDecrease(dmg);
            target.status.element.execution = true;
            target.buffCool(cooltime, id);
        }
    }

    public override bool checkCombinate()
    {
        return false;
    }


}
