using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SlimePatturn1 : Patturn
{
    public override IEnumerator Action()
    {
        yield return 0;
    }

    public override void Update()
    {
        if (!isActivate && issetData)
        {
            isActivate = true;
            for(int i = 0; i < 360; i += 36)
            {
                GameObject go = GameManager.Resource.Instantiate("Mobs/Danmaku");
                go.GetComponent<Danmaku>().setData(mob);
                float degrees = Mathf.Repeat(i, 360f);
                float radianAngle = Mathf.Deg2Rad * degrees;
                Vector3 dir = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
                dir.Normalize();
                go.GetComponent<Danmaku>().setTrans(dir.x, dir.y, 0, 5f);
            }
        }
        else
        {
            GameManager.Resource.Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isActivate = false;
    }

}
