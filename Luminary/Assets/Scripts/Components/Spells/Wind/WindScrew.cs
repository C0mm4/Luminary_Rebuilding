using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScrew : Projectile
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        transform.localEulerAngles = Vector3.zero;
        transform.Rotate(Vector3.back, 90);
        transform.Rotate(Vector3.left, 60);
    }

    public override void Debuffs(GameObject target)
    {
        base.Debuffs(target);
        new Ignite(target.GetComponent<Charactor>(), player.GetComponent<Charactor>(), dmg);
    }
}
