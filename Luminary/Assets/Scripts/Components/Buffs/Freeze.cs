using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Buff
{
    public Freeze(Charactor tar, Charactor atk, int dmg) : base(tar, atk)
    {   
        id = 1;
        setDurate(999f);
//        setDurate(2f);
        setTickTime(0f);

        cooltime = 5f;

        this.dmg = 1 * (100 + attacker.GetComponent<Player>().status.freezeDMG) / 100; ;

        startEffect();
    }

    public override void startEffect()
    {
        if (!target.status.element.freeze && !checkCombinate())
        {
            base.startEffect();
            if (target.isboss)
            {
                target.status.pGetDMG += 3;
                target.calcStatus();
            }
            else
            {
                Debug.Log("Stun");
                target.changeState(new MobStunState(2f));
            }
            target.HPDecrease(dmg);
            target.status.element.freeze = true;
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
            Buff newbuff = new Melting(target, attacker, dmg);
            return true;
        }        
        // Find Flow Buff
        buff = buffs.Find(buff => buff.id == 2);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Expand(target, attacker, dmg);
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
        // Find Elect Buff
        buff = buffs.Find(buff => buff.id == 4);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new ElectShock(target, attacker, dmg);
            return true;
        }

        // Find Seed Buff
        buff = buffs.Find(buff => buff.id == 5);
        if (buff != null)
        {
            buff.endEffect();
            Buff newbuff = new Rooted(target, attacker, dmg);
            return true;
        }


        return false;
    }

    public override void endEffect()
    {
        if(target.isboss)
        {
            target.status.pGetDMG -= 3;
            target.calcStatus();
        }
        else
        {
        }
        base.endEffect();
    }

}
