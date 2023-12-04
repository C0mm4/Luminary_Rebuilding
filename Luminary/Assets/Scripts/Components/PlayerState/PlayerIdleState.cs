using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        charactor.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

    }

    public override void UpdateState()
    {
        charactor.AnimationPlay("IdleAnimation");
    }

    public override void ExitState()
    {
        charactor = null;
    }

    public override void ReSetState(Charactor chr)
    {
        EnterState(chr);
    }
}
