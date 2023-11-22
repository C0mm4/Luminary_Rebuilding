using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diffusion : Buff
{
    public Diffusion(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 21;
        setDurate(0f);
        setTickTime(0f);

        cooltime = 20f;

        this.dmg = 1;

        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.diffusion)
        {
            base.startEffect();
            target.HPDecrease(dmg);
            target.status.element.diffusion = true;
            target.buffCool(cooltime, id);
        }
    }
    public override bool checkCombinate()
    {
        return false;
    }

    public override void endEffect()
    {
        GameObject go = GameManager.Resource.Instantiate("Spell/Field/Buff/Diffusion");
        base.endEffect();

    }
}
