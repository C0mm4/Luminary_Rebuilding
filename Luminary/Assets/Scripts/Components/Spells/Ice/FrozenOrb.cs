using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenOrb : Projectile
{
    float rotateAngle;
    public override void Start()
    {
        intpower = 4;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        transform.localEulerAngles = Vector3.zero;
        transform.Rotate(Vector3.forward, rotateAngle);
        transform.Rotate(Vector3.left, 60);

        rotateAngle += 360 * Time.deltaTime;
    }
    public override void Debuffs(GameObject target)
    {
        base.Debuffs(target);
        new Freeze(target.GetComponent<Charactor>(), player.GetComponent<Charactor>(), dmg);
    }

    public override void setDMG()
    {
        dmg = ((data.damage + player.GetComponent<Player>().status.Intellect * 10) * player.GetComponent<Player>().status.finalDMG);
    }
}
