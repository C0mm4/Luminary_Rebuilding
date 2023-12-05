using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectile
{
    float rotateAngle;
    // Start is called before the first frame update
    public override void Start()
    {

        intpower = 10;
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
        new Ignite(target.GetComponent<Charactor>(), player.GetComponent<Charactor>(), dmg);
    }

}
