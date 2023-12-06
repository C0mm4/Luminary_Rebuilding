using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : Buff
{
    public Flow(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 2;
        setDurate(999f);
//        setDurate(5f);
        setTickTime(0f);

        cooltime = 5f;

        this.dmg = 1 * (100 + attacker.GetComponent<Player>().status.flowDMG) / 100; ;
        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.flow && !checkCombinate())
        {
            base.startEffect();
            if (target.isboss)
            {
                target.status.pIncreaseSpeed -= 5;
            }
            else
            {
                target.status.pIncreaseSpeed -= 20;
            }
            target.calcStatus();
            target.HPDecrease(dmg);
            target.status.element.flow = true;
            target.buffCool(cooltime, id);
        }
    }

    public override void endEffect()
    {
        if (target.isboss)
        {
            target.status.pIncreaseSpeed += 5;
        }
        else
        {
            target.status.pIncreaseSpeed += 20;
        }
        target.calcStatus();
        base.endEffect();
    }
    public override bool checkCombinate()
    {
        List<Buff> buffs = target.GetComponent<Charactor>().status.buffs;
        // Find Fire Buff
        Buff buff = buffs.Find(buff => buff.id == 0);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Burnning(target, attacker, dmg);
            return true;
        }
        // Find Freeze Buff
        buff = buffs.Find(buff => buff.id == 1);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Expand(target, attacker, dmg);
            return true;
        }
        // Find Shock Buff
        buff = buffs.Find(buffs => buff.id == 3);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Weathering(target, attacker, dmg);
            return true;
        }
        // Find Elect Buff
        buff = buffs.Find(buffs => buff.id == 4);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Overloading(target, attacker, dmg);
            return true;
        }
        // Find Seed Buff
        buff = buffs.Find(buffs => buff.id == 5);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Diffusion(target, attacker, dmg);
            return true;
        }
        return false;
    }
}
