using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentence : Buff
{

    public Sentence(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 6;
        this.dmg = 1;
        setDurate(999f);
//        setDurate(5f);
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
            target.status.pGetDMG += 2;
            target.calcStatus();
            text = instance.GetType().Name + " X" + stack.ToString();
            base.startEffect();
        }
    }

    public override void resetEffect(int i)
    {
        stack = target.status.buffs[i].stack + 1;
        target.status.pGetDMG += 2;
        target.calcStatus();
        text = instance.GetType().Name + " X" + stack.ToString();
        base.resetEffect(i);
    }

    public override void endEffect()
    {
        target.status.pGetDMG -= 2 * stack;
        target.calcStatus() ;
        base.endEffect();
    }
    public override bool checkCombinate()
    {
        List<Buff> buffs = target.GetComponent<Charactor>().status.buffs;
        Buff buff = buffs.Find(buff => buff.id == 7);
        if (buff != null)
        {
            int targetstack = buff.stack;
            buff.endEffect();
            Buff newbuff = new Execution(target,attacker, targetstack + 1, dmg);
            return true;
        }
        return false;
    }

}
