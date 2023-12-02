using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmaku : MobProjectile
{
    public float Speed;
    public float activeSec;

    public void setTrans(float x, float y, float sec, float Speed)
    {
        activeSec = sec;
        this.Speed = Speed;
        dir = new Vector2(x*Speed, y*Speed);
        StartCoroutine(setActive());
    }

    public IEnumerator setActive()
    {
        yield return new WaitForSeconds(activeSec);
        isThrow = true;
    }
}
