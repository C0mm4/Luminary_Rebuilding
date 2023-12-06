using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Patturn : MonoBehaviour
{
    public Mob mob;
    public bool issetData;
    public bool isActivate;

    // Start is called before the first frame update
    void Start()
    {
        isActivate = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        try
        {
            if (mob.gameObject == null || mob.status.currentHP <= 0)
            {
                GameManager.Resource.Destroy(gameObject);
            }

        }
        catch 
        {
            GameManager.Resource.Destroy(gameObject);
        }
    }
        
    public void setData(Mob mob)
    {
        this.mob = mob;
        issetData = true;
    }

    public abstract IEnumerator Action();
}
