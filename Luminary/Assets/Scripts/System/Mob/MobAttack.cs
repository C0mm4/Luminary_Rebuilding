using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MobAttack : MonoBehaviour
{
    public Mob shooter;
    public MobAttack instance;

    public void Awake()
    {
        instance = this;
    }


    public void Destroy()
    {
        int targetid = shooter.AtkObj.FindIndex(item => item.instance.Equals(instance));
        shooter.AtkObj.Remove(instance);
        GameManager.Resource.Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Collision");
            other.GetComponent<Charactor>().HPDecrease(1);
        }
    }

    public virtual void Activate()
    {

    }

    public virtual void setData(Mob mob)
    {
        shooter = mob;
    }

    public virtual void setData(Mob mob, Vector3 pos)
    {
        shooter = mob;
    }

}
