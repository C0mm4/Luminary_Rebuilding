using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weathering : Buff
{
    public Weathering(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 19;
        setDurate(8f);
        setTickTime(0);

        cooltime = 15f;

        dmg = 1 * (100 + attacker.GetComponent<Player>().status.weatheringDMG) / 100; ;
        startEffect();
    }

    public override void startEffect()
    {
        if(!target.status.element.weathering)
        {
            base.startEffect();
            target.element.weathering = true;
            target.buffCool(cooltime, id);
            target.HPDecrease(dmg);
            if (target.isboss)
            {
                target.status.pIncreaseSpeed -= 10;
                target.status.pIncreaseDMG -= 15;
            }
            else
            {
                target.status.pIncreaseSpeed -= 30;
            }
            target.calcStatus();
        }
    }

    public override void endEffect()
    {
        if(target.isboss) 
        {
            target.status.pIncreaseDMG += 15;
            target.status.pIncreaseSpeed += 10;
        }
        else
        {
            target.status.pIncreaseSpeed += 30;
        }
        target.calcStatus();
        base.endEffect();
    }
    public override bool checkCombinate()
    {
        return false;
    }

}
