using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHand : Projectile
{
    // Start is called before the first frame update
    public override void Start()
    {
        intpower = 5;
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
        new Shock(target.GetComponent<Charactor>(), player.GetComponent<Charactor>(), dmg);
    }
}