using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MobProjectile : MobAttack
{
    public Vector3 dir;
    public bool isGravity;
    public bool isThrow;
    public bool trajectory;
    public float timeOfFlight;
    public Charactor player;

    // Start is called before the first frame update
    void Start()
    {
        isThrow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shooter == null)
        {
            GameManager.Resource.Destroy(gameObject);
        }
        if (isThrow)
        {
            if (!trajectory)
            {
                // Throw objects
                GetComponent<Rigidbody2D>().velocity = dir;
                transform.eulerAngles = new Vector3(0, 0, 0);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.Rotate(Vector3.forward, angle);
                transform.Rotate(Vector3.left, 60);
                if (isGravity)
                {
                    dir.y -= Time.deltaTime * 10;
                }
            }
            else
            {
                transform.position += Time.deltaTime * dir;
                float angleXY = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                float angleXZ = Mathf.Atan2(dir.z, dir.y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.Rotate(Vector3.forward, angleXY);
                transform.Rotate(Vector3.up, angleXZ);
                transform.Rotate(Vector3.left, 60);
                dir.z += Time.deltaTime * 10;
            }
        }
    }

    public override void setData(Mob mob)
    {
        // Set Target, attackers, transforms
        base.setData(mob);
        player = shooter.player;
        transform.position = shooter.transform.position;
    }

    public override void Activate()
    {
        isThrow = true;
        if (trajectory)
        {
            // Calculate initial velocity
            Vector3 displacement = player.transform.position - transform.position;
            Vector3 initialVelocity = displacement / 2f;


            dir = new Vector3(initialVelocity.x, initialVelocity.y, -10f);

        }
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wall")
        {
            GameManager.Resource.Destroy(gameObject);
        }
        base.OnTriggerEnter2D(other);
    }
}
