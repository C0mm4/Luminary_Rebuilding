using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracked : Buff
{
    public Cracked(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 16;
        setDurate(3f);
        setTickTime(0f);

        this.dmg = 1 * (100 + tar.GetComponent<Player>().status.crackedDMG) / 100;

        target.calcStatus();
        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.cracked)
        {
            base.startEffect();
            target.HPDecrease(dmg);
            target.status.pIncreaseDMG -= 50;
            target.status.element.cracked = true;
            target.buffCool(cooltime, id);
        }
    }

    public override void endEffect()
    {
        target.status.pIncreaseDMG += 50;
        target.calcStatus();
        base.endEffect();
    }
    public override bool checkCombinate()
    {
        return false;
    }


}
