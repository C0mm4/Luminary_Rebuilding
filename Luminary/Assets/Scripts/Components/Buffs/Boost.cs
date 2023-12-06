using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : Buff
{
    public Boost(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 24;
        setDurate(0f);
        setTickTime(0f);

        cooltime = 20;

        this.dmg = (int)Mathf.Floor(target.status.maxHP / 5) * (100 + attacker.GetComponent<Player>().status.boostDMG) / 100;

        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.boost)
        {
            base.startEffect();
            target.status.element.boost = true;
            target.buffCool(cooltime, id);
        }
    }

    public override bool checkCombinate()
    {
        return false;
    }

    public override void endEffect()
    {
        GameObject go = GameManager.Resource.Instantiate("Spell/Field/Buff/BoostField");
        base.endEffect();
    }

}
