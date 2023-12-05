using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : Buff
{
    public Shock(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 3;
        setDurate(999f);
//        setDurate(3f);
        setTickTime(0f);

        cooltime = 5f;

        this.dmg = 1 * (100 + tar.GetComponent<Player>().status.shockDMG) / 100; ;
        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.shock && !checkCombinate())
        {
            base.startEffect();
            target.status.pIncreaseDMG -= 2;
            target.calcStatus();
            target.HPDecrease(dmg);
            target.status.element.shock = true;
            target.buffCool(cooltime, id);
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
            Buff newbuff = new Extinguish(target, attacker, dmg);
            return true;
        }
        // Find Shock Buff
        buff = buffs.Find(buff => buff.id == 3);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Cracked(target, attacker, dmg);
            return true;
        }
        // Find Seed Buff
        buff = buffs.Find(buffs => buff.id == 5);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Rooted(target, attacker, dmg);
            return true;
        }
        // Find Elect Buff
        buff = buffs.Find(buffs => buff.id == 4);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new ElectShock(target, attacker, dmg);
            return true;
        }
        // Find Flow Buff
        buff = buffs.Find(buffs => buff.id == 2);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Expand(target, attacker, dmg);
            return true;
        }

        return false;
    }

}
