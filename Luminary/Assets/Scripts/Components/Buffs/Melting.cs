using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melting : Buff
{
    public Melting(Charactor tar, Charactor atk, int dmg) : base(tar, atk) 
    {
        id = 10;
        setDurate(0);
        setTickTime(0);

        cooltime = 10f;

        this.dmg = 30 + (5 * dmg) * (100 + attacker.GetComponent<Player>().status.meltingDMG) / 100; ;
        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.melting)
        {
            base.startEffect();
            target.status.element.melting = true;
            target.buffCool(cooltime, id);
        }
    }

    public override bool checkCombinate()
    {
        return false;
    }

    public override void endEffect()
    {
        target.HPDecrease(dmg);
        base.endEffect();
    }
}
