using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmaku : MobProjectile
{
    public float Speed;
    public float activeSec;

    public void setTrans(float x, float y, float sec, float Speed, float scale = 1)
    {
        activeSec = sec;
        this.Speed = Speed;
        dir = new Vector2(x*Speed, y*Speed);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(scale, scale * 2, 1);
        transform.Rotate(Vector3.forward, angle);
        transform.Rotate(Vector3.left, 60);
        StartCoroutine(setActive());
    }

    public IEnumerator setActive()
    {
        yield return new WaitForSeconds(activeSec);
        isThrow = true;
    }
}
