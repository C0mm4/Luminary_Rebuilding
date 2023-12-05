using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatturn3 : Patturn
{
    public Vector3 dir;
    public override void Update()
    {
        dir = GameManager.player.transform.position - mob.transform.position;
        mob.GetComponent<Rigidbody2D>().velocity = dir * 3;
        StartCoroutine(Action());
    }

    
    public override IEnumerator Action()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Resource.Destroy(gameObject);
    }

}
