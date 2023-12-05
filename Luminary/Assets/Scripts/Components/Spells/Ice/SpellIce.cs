using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellIce : Projectile
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
        new Freeze(target.GetComponent<Charactor>(), player.GetComponent<Charactor>(), dmg);
    }
}
