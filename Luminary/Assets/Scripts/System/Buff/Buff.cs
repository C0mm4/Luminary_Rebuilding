using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Buff
{
    public Charactor target;
    public Charactor attacker;

    public float startTime;
    public float currentTime;

    public float tickTime;
    public float lastTickTime;

    public int dmg;
    public float durate;

    public Buff instance;

    public float cooltime;

    Sprite img;

    public string text;

    public int id;
    public int stack = 0;
    // Generate Buff objects setting on target, and attacker
    public Buff(Charactor tar, Charactor atk)
    {
        // base.Buff(tar, atk)
        // 
        // Extention Contents
        target = tar;
        attacker = atk;
        instance = this;
    }
    // Set Buffs Durae Time
    public void setDurate(float d)
    {
        durate = d;
    }
    // Set Buffs Tick time
    public void setTickTime(float t)
    {
        tickTime = t;
    }
    // Buffs Start Effect
    public virtual void startEffect()
    {
        int index = target.status.buffs.FindIndex(buff => buff.id == id);
        if (index == -1)
        {
            // if same buffs doesn't exist on same target
            // Add buffs
            target.status.buffs.Add(instance);
        }
        else
        {
            // if same buffs already exist on same target
            // buffs reset
            resetEffect(index);
        }
        // Set Start Time, Tick Time
        startTime = Time.time;
        lastTickTime = startTime;
        
    }

    public virtual void durateEffect()
    {

        // Extenttion Contents
        //
        // base.durateEffect();

        // if durate time is over, end buffs
        currentTime = Time.time;
        if(currentTime - startTime >= durate)
        {
            target.status.endbuffs.Add(instance);
        }
    }

    // tick buffs handler
    public virtual void onTick()
    {
    }

    public virtual void resetEffect(int i)
    {
        //
        // Extention Contents
        //
        // base.resetEffect();

        // delete already exists same buffs, and register new buffs
        target.status.buffs.RemoveAt(i);
        target.status.buffs.Add(instance);
    }

    public virtual void endEffect()
    {
        //
        // Extention Contents
        //
        // base.endEffect();

        // if buffs finish, remove buff objects
        int targetid = target.status.buffs.FindIndex(item => item.instance.Equals(instance));
        target.status.buffs.RemoveAt(targetid);
    }

    public abstract bool checkCombinate();
}
