using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobRunState : State
{
    Vector3 dir = new Vector3();
    Vector3 pastPos = new Vector3();
    public override void EnterState(Charactor chr)
    {
        charactor = chr;
        pastPos = charactor.transform.position;
    }

    public override void ExitState()
    {
        charactor = null;
    }

    public override void ReSetState(Charactor chr)
    {
        EnterState(chr);
    }

    public override void UpdateState()
    {
        charactor.AnimationPlay("MoveAnimation");
        if(GameManager.player == null)
        {
            charactor.GetComponent<Charactor>().endCurrentState();
        }
        else
        {
            dir = new Vector3(charactor.transform.position.x - charactor.player.transform.position.x,
                        charactor.transform.position.y - charactor.player.transform.position.y);
            dir.Normalize();
            charactor.charactorSpeed = dir;
            if(Mathf.Abs(charactor.transform.position.x - pastPos.x) <= 0f)
            {
                dir.x = -dir.x;
            }
            if(Mathf.Abs(charactor.transform.position.y - pastPos.y) <= 0f)
            {
                dir.y = - dir.y;
            }

            pastPos = charactor.transform.position;
            //            charactor.GetComponent<Rigidbody2D>().velocity = dir * charactor.status.speed;
            charactor.transform.position += dir * charactor.status.speed * Time.deltaTime;
            Debug.Log(dir);
            charactor.GetComponent<Charactor>().sawDir.x = Vector2.Dot(charactor.GetComponent<Charactor>().charactorSpeed, new Vector2(1, 0));
        }
    }

}
