using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooted : Buff
{
    public Rooted(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 16;
        setDurate(0f);
        setTickTime(0f);

        cooltime = 10f;

        this.dmg = 1 *(100 + attacker.GetComponent<Player>().status.rootedDMG) / 100; ;

        startEffect();
        
    }

    public override void startEffect()
    {
        if(!target.status.element.rooted)
        {
            base.startEffect();
            target.HPDecrease(dmg);
            target.status.element.rooted = true;
            target.buffCool(cooltime, id);
        }
    }

    public override bool checkCombinate()
    {
        return false;
    }

    public override void endEffect()
    {
        GameObject go = GameManager.Resource.Instantiate("Spell/Field/Buff/RootedField");
        base.endEffect();
    }

}
