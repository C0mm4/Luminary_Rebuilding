using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePillar : AIModel
{
    public bool isSet = false;
    public override void Update()
    {
        if (target.GetComponent<DownFall>().isFallEnd)
        {
            if (isSet)
            {
                if (Time.time - target.lastAttackT[0] >= 3f)
                {
                    target.changeState(new MobCastState(1f, 0));
                }
            }
            else
            {
                timeSet();
            }
        }
    }

    public void timeSet()
    {
        target.lastAttackT[0] = Time.time;
        isSet = true;
    }
}
