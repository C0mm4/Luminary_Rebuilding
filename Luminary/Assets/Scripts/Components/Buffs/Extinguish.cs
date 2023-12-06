using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguish : Buff
{
    public Extinguish(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 12;
        setDurate(3.2f);
        setTickTime(1f);

        cooltime = 10f;

        this.dmg = 10 + 2 * dmg * (100 + attacker.GetComponent<Player>().status.extinguishDMG) / 100; ;
        target.calcStatus();
        startEffect();
    }

    public override void startEffect()
    {
        if(!target.status.element.extinguish)
        {
            base.startEffect();
            target.status.finalDMG -= 10;
            target.status.element.extinguish = true;
            target.buffCool(cooltime, id);
        }
    }

    public override bool checkCombinate()
    {
        return false;
    }

    public override void durateEffect()
    {
        if (target != null)
        {

            if (currentTime - lastTickTime >= tickTime)
            {
                onTick();
                lastTickTime = currentTime;
            }
            base.durateEffect();

        }
        else
        {
            target = null;
            attacker = null;
        }
    }

    public override void onTick()
    {
        target.HPDecrease(dmg);

        base.onTick();
    }

    public override void endEffect()
    {
        target.status.finalDMG += 10;
        target.calcStatus() ;

        base.endEffect();
    }
}
