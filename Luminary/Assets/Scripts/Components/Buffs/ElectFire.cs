using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectFire : Buff
{
    public ElectFire(Charactor tar, Charactor atk) : base(tar, atk) 
    {
        id = 13;
        setDurate(0);
        setTickTime(0);
            
        cooltime = 10f;

        this.dmg = target.isboss ? (target.status.maxHP / 10) * (100 + tar.GetComponent<Player>().status.electFireDMG) / 100 : 20 * (100 + tar.GetComponent<Player>().status.electFireDMG) / 100; ;

        startEffect();
    }

    public override void startEffect()
    {
        if(!target.status.element.electfire)
        {
            base.startEffect();
            target.status.element.electfire = true;
            target.buffCool(cooltime, id);
        }
    }

    public override bool checkCombinate()
    {
        return false;
    }

    public override void endEffect()
    {
        if(target.isboss)
        {
            target.TrueDMG(dmg);
        }
        else
        {
            target.HPDecrease(dmg);
        }
        base.endEffect();
    }
}