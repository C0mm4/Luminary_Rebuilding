using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : Buff
{
    public Electric(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 4;
        setDurate(999f);
//        setDurate(6f);
        setTickTime(0f);

        cooltime = 10f;

        this.dmg = 1 * (100 + attacker.GetComponent<Player>().status.electDMG) / 100;
        startEffect();
    }

    public override void startEffect()
    {
        if(!target.status.element.electric && !checkCombinate())
        {
            if (!target.isboss)
            {
                target.changeState(new MobStunState(0.5f));
            }
            target.status.element.electric = true;
            target.buffCool(cooltime, id);
            target.HPDecrease(dmg);
        }
    }

    public override bool checkCombinate()
    {
        List<Buff> buffs = target.GetComponent<Charactor>().status.buffs;

        // Find Fire Buff
        Buff buff = buffs.Find(buff => buff.id == 0);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new ElectFire(target, attacker);
            return true;
        }
        // Find Freeze Buff
        buff = buffs.Find(buff => buff.id == 1);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new ElectShock(target, attacker, dmg);
            return true;
        }
        // Find Flow Buff
        buff = buffs.Find(buff => buff.id == 2);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Overloading(target, attacker, dmg);
            return true;
        }
        // Find Shock Buff
        buff = buffs.Find(buff => buff.id == 3);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Discharge(target, attacker, dmg);
            return true;
        }

        // Find Seed Buff
        buff = buffs.Find(buff => buff.id == 5);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Boost(target, attacker, dmg);
            return true;
        }
        return false;
    }


}
