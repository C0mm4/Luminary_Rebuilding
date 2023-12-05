using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : Buff
{
    public Seed(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 5;
        setDurate(999f);
//        setDurate(10f);
        setTickTime(0);

        cooltime = 20;
        this.dmg = 1 * (100 + tar.GetComponent<Player>().status.seedDMG) / 100; ;

        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.seed && !checkCombinate())
        {
            base.startEffect();
            target.HPDecrease(dmg);
            target.status.element.seed = true;
            target.buffCool(cooltime, id);
        }
    }

    public override bool checkCombinate()
    {
        List<Buff> buffs = target.GetComponent<Charactor>().status.buffs;       
        // Find Ignite Buff
        Buff buff = buffs.Find(buff => buff.id == 0);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Fire(target, attacker, dmg);
            return true;
        }
        // Find Freeze Buff
        buff = buffs.Find(buff => buff.id == 1);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Rooted(target, attacker, dmg);
            return true;
        }
        // Find Flow Buff
        buff = buffs.Find(buff => buff.id == 2);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Diffusion(target, attacker, dmg);
            return true;
        }
        // Find Shock Buff
        buff = buffs.Find(buff => buff.id == 3);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Sprout(target, attacker, dmg);
            return true;
        }
        // Find Elect Buff
        buff = buffs.Find(buff => buff.id == 4);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new ElectFire(target, attacker);
            return true;
        }

        return false;
    }


}
