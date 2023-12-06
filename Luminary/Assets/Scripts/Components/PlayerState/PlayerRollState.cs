using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : State
{
    // Start is called before the first frame update

    Vector2 dir = Vector2.one;
    public PlayerRollState()
    {

    }

    public PlayerRollState(Vector2 dir, GameObject obj)
    {
        this.dir = dir;
    }

    public override void EnterState(Charactor chr)
    {
        charactor = chr;

        
        charactor.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        charactor.AnimationPlay("TeleportAnimation", 1f);

        charactor.GetComponent<Player>().telpoDir = dir;
    }
    
    public override void UpdateState()
    {
    }

    public override void ReSetState(Charactor chr)
    {
        EnterState(chr);
    }

    public override void ExitState()
    {
        charactor = null;
        
    }
}
