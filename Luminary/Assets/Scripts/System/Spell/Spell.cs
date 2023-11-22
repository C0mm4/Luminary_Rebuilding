using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public SpellData data;

    // Create Spell Objects
    public virtual void execute(Vector3 mos) 
    {
        GameObject obj = GameManager.Resource.Instantiate(data.path);
        obj.GetComponent<SpellObj>().setData(data, mos);

    }

    public void setData(SpellData dt)
    {
        data = dt;
    }


}
