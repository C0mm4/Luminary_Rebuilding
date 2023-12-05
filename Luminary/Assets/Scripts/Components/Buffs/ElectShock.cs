using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectShock : Buff
{
    public ElectShock(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 17;
        setDurate(1f);
        setTickTime(0f);

        cooltime = 10f;

        this.dmg = 5 * (100 + tar.GetComponent<Player>().status.electShockDMG) / 100; 
        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.electshock)
        {
            base.startEffect();
            if (!target.isboss)
            {
                target.changeState(new MobStunState());
            }
            target.HPDecrease(dmg);
            target.status.element.electshock = true;
            target.buffCool(cooltime, id);
        }
    }

    public override bool checkCombinate()
    {
        return false;
    }

    public override void endEffect()
    {
        if (!target.isboss)
        {
            target.endCurrentState();
        }
        else
        {
            target.TrueDMG(5);
        }
        base.endEffect();
    }

}
