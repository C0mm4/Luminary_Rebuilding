using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        chr.AnimationPlay("DeadAnimation");
        Debug.Log("DieState");
        chr.GetComponent<Rigidbody2D>().simulated = false;
    }

    public override void ExitState()
    {
        GameManager.Resource.Destroy(charactor.gameObject);
        charactor = null;
    }

    public override void ReSetState(Charactor chr)
    {

    }

    public override void UpdateState()
    {

    }

}
