using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Mob Attack Objects
public class GoblinArrow : MobProjectile
{
    public override void setData(Mob mob)
    {
        base.setData(mob);
        if(shooter.sawDir.x < 0)
        {
            transform.position += new Vector3(-0.1f, 0.75f, -1.3f);
        }
        else
        {
            transform.position += new Vector3(0.1f, 0.75f, -1.3f);
        }
    }
    public override void Activate()
    {
        dir = new Vector3(player.transform.position.x - shooter.transform.position.x, player.transform.position.y - shooter.transform.position.y, 0);
        base.Activate();
    }
}
