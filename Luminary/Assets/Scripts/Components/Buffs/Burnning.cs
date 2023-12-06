using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnning : Buff
{

    public Burnning(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 11;
        setDurate(5.2f);
        setTickTime(1f);

        cooltime = 10f;

        this.dmg = (int)Math.Floor(1 + (0.1 * dmg)) * (100 + attacker.GetComponent<Player>().status.burnningDMG) / 100;
        startEffect();
    }

    public override void startEffect()
    {
        if(!target.status.element.burning)
        {
            base.startEffect();
            target.status.element.burning = true;
            target.buffCool(cooltime, id);
        }
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
    public override bool checkCombinate()
    {
        return false;
    }

}
