using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : Buff
{
    public Expand(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 15;
        setDurate(3f);
        setTickTime(0);

        cooltime = 10f;

        this.dmg = 1 * (100 + tar.GetComponent<Player>().status.expandDMG) / 100; ;

        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.expand)
        {
            base.startEffect();
            if (!target.isboss)
            {
                target.changeState(new MobStunState());
            }
            target.HPDecrease(dmg);
            target.status.pGetDMG += 30;
            target.calcStatus();
            target.status.element.expand = true;
            target.buffCool(cooltime, id);
        }
    }

    public override void endEffect()
    {
        target.status.pGetDMG -= 30;
        target.calcStatus();
        base.endEffect();
    }

    public override bool checkCombinate()
    {
        return false;
    }
}
