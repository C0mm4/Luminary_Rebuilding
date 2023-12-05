using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : Buff
{
    public Sprout(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 23;

        setDurate(0f);
        setTickTime(0f);

        cooltime = 20f;

        this.dmg = 1 * (100 + tar.GetComponent<Player>().status.sproutDMG) / 100; ;
        startEffect();
    }
    public override void startEffect()
    {
        if (!target.status.element.sprout)
        {
            base.startEffect();
            target.status.element.sprout = true;
            target.buffCool(cooltime, id);
        }
    }

    public override void endEffect()
    {
        GameObject go = GameManager.Resource.Instantiate("Spell/Field/Buff/SproutField");
        base.endEffect();
    }

    public override bool checkCombinate()
    {
        return false;

    }


}
