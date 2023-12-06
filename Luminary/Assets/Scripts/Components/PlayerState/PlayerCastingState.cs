using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingState : State
{
    Spell spell;
    GameObject target;
    float castingT;
    float startT;
    Vector3 mos = new Vector3();

    public PlayerCastingState(Spell spl, Vector3 mos) : base()
    {
        spell = spl;
        castingT = spl.data.castTime;
        this.mos = mos;
    }

    public PlayerCastingState(Spell spl, GameObject obj) : base()
    {
        spell = spl;
        castingT = spl.data.castTime;
        target = obj;
    }

    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        charactor.status.currentMana -= spell.data.circle;
        GameManager.player.GetComponent<Player>().lastCastTime = Time.time;
        startT = Time.time;
        charactor.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GameManager.Instance.uiManager.stableUI.GetComponent<StableUI>().isCast = true;
        GameManager.Instance.uiManager.stableUI.GetComponent<StableUI>().setCast(castingT, startT);

        charactor.GetComponent<Charactor>().sawDir.x = Vector2.Dot(mos - charactor.transform.position, new Vector2(1, 0));

        // Spell Sound Play
        GameManager.soundManager.playAttackSound();
    }

    public override void UpdateState()
    {
        charactor.AnimationPlay("CastAnimation", 1 / castingT);
        if(Time.time - startT >= castingT)
        {
            charactor.GetComponent<Charactor>().endCurrentState();
            spell.execute(mos);
        }
    }

    public override void ReSetState(Charactor chr)
    {
        EnterState(chr);
    }

    public override void ExitState()
    {
        GameManager.Instance.uiManager.stableUI.GetComponent<StableUI>().isCast = false;
        charactor = null;
    }
}
