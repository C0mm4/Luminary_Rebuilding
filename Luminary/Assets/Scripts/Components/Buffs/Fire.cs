using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Buff
{

    public Fire(Charactor tar, Charactor atk, int dmg) : base(tar, atk) 
    {
        id = 14;
        setDurate(0.1f);
        setTickTime(0f);

        cooltime = 10f;

        this.dmg = 3 * dmg * (100 + attacker.GetComponent<Player>().status.fireDMG) / 100; ;
        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.fire)
        {
            base.startEffect();
            target.status.element.fire = true;
            target.buffCool(cooltime, id);
        }
    }

    public override bool checkCombinate()
    {
        return false;
    }

    public override void endEffect()
    {
        GameObject go = GameManager.Resource.Instantiate("Spell/Field/Buff/FireField");
        go.GetComponent<Field>().dmg = dmg;
        base.endEffect();
    }
}
