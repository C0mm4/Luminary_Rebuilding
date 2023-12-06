using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite : Buff
{
    public Ignite(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {
        id = 0;
        setDurate(999f);
//        setDurate(3.2f);
        setTickTime(1f);

        cooltime = 5f;

        this.dmg = (int)Math.Floor(1 + (0.1f * dmg)) * (100 + attacker.GetComponent<Player>().status.igniteDMG) / 100; ;

        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.ignite && !checkCombinate())
        {
            base.startEffect();
            target.status.element.ignite = true;
            target.buffCool(cooltime, id);
        }

    }

    public override void durateEffect()
    {
        if (target != null)
        {
            if (Time.time - lastTickTime >= tickTime)
            {
                onTick();
                lastTickTime = Time.time;
            }

        }
        else
        {
            target = null;
            attacker = null;
        }
        base.durateEffect();
    }

    public override void onTick()
    {
        target.HPDecrease(dmg);

        base.onTick();
    }

    public override void endEffect()
    {
        base.endEffect();
    }

    public override bool checkCombinate()
    {
        List<Buff> buffs = target.GetComponent<Charactor>().status.buffs;
        // Find Freeze Buff
        Buff buff = buffs.Find(buff => buff.id == 1);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Melting(target, attacker, dmg);
            return true;
        }
        // Find Flow Buff
        buff = buffs.Find(buff => buff.id == 2);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Burnning(target, attacker, dmg);
            return true;
        }
        // Find Shock Buff
        buff = buffs.Find(buff => buff.id == 3);
        if ( buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Extinguish(target, attacker, dmg);
            return true;
        }
        // Find Elect Buff
        buff = buffs.Find(buff => buff.id == 4);
        if(buff != null)
        {
            buff.endEffect();
            Buff newbuff = new ElectFire(target, attacker);
            return true;
        }        
        // Find Seed Buff
        buff = buffs.Find(buff => buff.id == 5);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Fire(target, attacker, dmg);
            return true;
        }
        return false;
    }
}
