using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArrow : Projectile
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
    }

    public override void Debuffs(GameObject target)
    {
        base.Debuffs(target);
        new Flow(target.GetComponent<Charactor>(), player.GetComponent<Charactor>(), dmg);
    }
}
