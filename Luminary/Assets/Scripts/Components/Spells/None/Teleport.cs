using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Projectile
{
    public bool dataSet;
    public Vector3 dirs;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GetComponent<Rigidbody2D>().simulated = false;
        transform.eulerAngles = Vector3.zero;
        float angle = Mathf.Atan2(dirs.y, dirs.x) * Mathf.Rad2Deg;
        transform.position = player.transform.position + new Vector3(Mathf.Cos(angle) * 0.45f, Mathf.Sin(angle) * 0.6f, 0);

    }

    // Update is called once per frame
    public override void Update()
    {
    }
}
