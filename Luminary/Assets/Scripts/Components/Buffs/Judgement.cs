using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement : Buff
{
    public Judgement(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 7;
        this.dmg = 1;
        setDurate(5f);
        setTickTime(0);

        cooltime = 0;
        startEffect();
    }

    public override void startEffect()
    {
        if (!checkCombinate())
        {
            stack = 1;
            target.HPDecrease(dmg);
            target.status.pIncreaseSpeed -= 5;
            target.calcStatus();
            text = instance.GetType().Name + " X" + stack.ToString();
            base.startEffect();
        }
    }

    public override void resetEffect(int i)
    {
        stack = target.status.buffs[i].stack + 1;
        text = instance.GetType().Name + " X" + stack.ToString();
        target.status.pIncreaseSpeed -= 5;
        target.calcStatus();
        base.resetEffect(i);
    }

    public override bool checkCombinate()
    {
        List<Buff> buffs = target.GetComponent<Charactor>().status.buffs;
        Buff buff = buffs.Find(buff => buff.id == 6);
        if (buff != null)
        {
            int targetstack = buff.stack;
            buff.endEffect();
            Buff newbuff = new Execution(target, attacker, targetstack + 1, dmg);
            return true;
        }
        return false;
    }

}
