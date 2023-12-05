using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : SpellObj
{
    [SerializeField]
    float speed;
    [SerializeField]
    float TickTime = 1f;
    float lastTickTime;

    List<KeyValuePair<GameObject, float>> dmgList = new List<KeyValuePair<GameObject, float>>();

    public override void Start()
    {
        base.Start();

    }

    public override void Update()
    {
        base.Update();

        if (Time.time - spawnTime > data.durateT)
        {
            GameManager.Resource.Destroy(gameObject);
        }
        List<KeyValuePair<GameObject, float>> delList = new List<KeyValuePair<GameObject, float>>();
        foreach(var kvp in dmgList)
        {
            if(Time.time - kvp.Value >= TickTime)
            {
                delList.Add(kvp);
            }
        }
        foreach(var kvp in delList)
        {
            dmgList.Remove(kvp);
        }
        
    }




    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Mob")
        {
            if(!IsObjInList(other.gameObject))
            {
                KeyValuePair<GameObject, float> kv = new KeyValuePair<GameObject, float>(other.gameObject, Time.time);
                dmgList.Add(kv);
                setDMG();
                other.GetComponent<Charactor>().HPDecrease(dmg);
                Debuffs(other.gameObject);
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {

    }

    public bool IsObjInList(GameObject tar)
    {
        foreach(var go in dmgList)
        {
            if(go.Key.GetHashCode() == tar.GetHashCode())
            {
                return true;
            }
        }
        return false;
    }
}