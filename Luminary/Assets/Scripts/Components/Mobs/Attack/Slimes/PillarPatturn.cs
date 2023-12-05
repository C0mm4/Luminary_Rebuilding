using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarPatturn : Patturn
{
    public override IEnumerator Action()
    {
        yield return 0;
        GameManager.Resource.Destroy(gameObject);
    }

    public override void Update()
    {
        base.Update();
        if (!isActivate && issetData)
        {
            isActivate = true;
            Vector3 dir = GameManager.player.transform.position - mob.transform.position;
            float degrees = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
            for(float angle = degrees - 30; angle <= degrees + 30; angle += 30)
            {

                GameObject go = GameManager.Resource.Instantiate("Mobs/Danmaku");
                go.GetComponent<Danmaku>().setData(mob);
                float radianAngle = Mathf.Deg2Rad * angle;
                Vector3 dDir = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
                dir.Normalize();
                go.GetComponent<Danmaku>().setTrans(dDir.x, dDir.y, 0, 3f, 0.25f);
            }
            mob.status.currentHP += 30;
            mob.setIdleState();
            GameManager.Resource.Destroy(gameObject);
        }
    }

}
