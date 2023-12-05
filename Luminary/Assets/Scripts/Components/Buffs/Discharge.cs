using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discharge : Buff
{
    public Discharge(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 22;
        setDurate(6f);
        setTickTime(0);

        cooltime = 15f;

        dmg = target.status.maxHP / 20 * (100 + tar.GetComponent<Player>().status.dischargeDMG) / 100;

        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.discharge)
        {
            base.startEffect();
            target.status.element.discharge = true;
            target.buffCool(cooltime, id);
            if(target.isboss)
            {
                target.TrueDMG(10);
            }
            else
            {
                target.HPDecrease(dmg);
            }
            target.status.pIncreaseDMG -= 10;
            target.calcStatus();
        }
    }
    public override void endEffect()
    {
        target.status.pIncreaseDMG += 10;
        target.calcStatus();
        base.endEffect();
    }

    public override bool checkCombinate()
    {
        return false;
    }


}
