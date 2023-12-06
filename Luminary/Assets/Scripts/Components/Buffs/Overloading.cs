using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overloading : Buff
{
    public Overloading(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 20;
        setDurate(6f);
        setTickTime(1f);

        cooltime = 10f;

        this.dmg = 2 * (100 + attacker.GetComponent<Player>().status.overloadDMG) / 100; ;

        startEffect();
    }

    public override  void startEffect()
    {
        if (!target.status.element.overloading)
        {
            base.startEffect();
            target.status.element.overloading = true;
            target.buffCool(cooltime, id);

        }
    }

    public override void durateEffect()
    {
        if (target != null)
        {

            if (currentTime - lastTickTime >= tickTime)
            {
                onTick();
                lastTickTime = currentTime;
            }
            base.durateEffect();

        }
        else
        {
            target = null;
            attacker = null;
        }
    }

    public override void onTick()
    {
        target.TrueDMG(dmg);
        base.onTick();
    }

    public override bool checkCombinate()
    {
        return false;
    }


}
