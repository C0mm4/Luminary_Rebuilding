using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMoveState : State
{
    
    Vector2 dir = new Vector2();
    int cnt = 0;
    public override void EnterState(Charactor chr)
    {
        charactor = chr;

    }
    public override void UpdateState()
    {
        charactor.AnimationPlay("MoveAnimation");
        if (charactor.GetComponent<Player>().ismove)
        {
            if(charactor.GetComponent<Player>().charactorSpeed == Vector2.zero) 
            {
                charactor.GetComponent<Charactor>().endCurrentState();
            }
            else
            {
                dir = charactor.GetComponent<Player>().charactorSpeed.normalized * charactor.GetComponent<Player>().charactorSpeed;
                charactor.GetComponent<Rigidbody2D>().velocity = charactor.GetComponent<Player>().charactorSpeed;
                charactor.GetComponent<Charactor>().sawDir.x = Vector2.Dot(charactor.GetComponent<Player>().charactorSpeed, new Vector2(1, 0));
                
            }
            
        }
        else
        {
            charactor.GetComponent<Charactor>().endCurrentState();
        }


    }
    public override void ReSetState(Charactor chr)
    {

    }

    public override void ExitState()
    {
        charactor.GetComponent<Player> ().ismove = false;
        charactor = null;
    }
}
