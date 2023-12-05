using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobField : MobAttack
{
    public Charactor player;
    public bool isActive = false;
    public GameObject ActiveObj = null;
    public GameObject Prefab;

    public bool isRandom;

    public float ActivateT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shooter == null)
        {
            GameManager.Resource.Destroy(gameObject);
        }
        if (isActive)
        {
            // Activate Hitbox Object
            if(ActiveObj == null)
            {
                ActiveObj = GameManager.Resource.Instantiate(Prefab, gameObject.transform);
                ActiveObj.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z -1);
                ActivateT = Time.time;
            }
            else
            {
                if(Time.time - ActivateT >= 1f) 
                {
                    Destroy();
                }
            }
        }
    }

    public override void setData(Mob mob, Vector3 pos)
    {
        // Set target, attackers and transform
        base.setData(mob, pos);
        player = shooter.player;
        transform.position = pos;
        Debug.Log("Field Set");
    }

    public override void setData(Mob mob)
    {
        base.setData(mob); 
        player = shooter.player;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
        Debug.Log("Field Set");
    }

    public virtual void setActive()
    {
        isActive = true;
    }

    public override void Activate()
    {
        base.Activate();
        isActive = true;
    }
}
