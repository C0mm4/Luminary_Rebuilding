using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public SpellData data;

    // Create Spell Objects
    public void execute(Vector3 mos) 
    {
        GameObject obj = GameManager.Resource.Instantiate(data.path);
        obj.GetComponent<SpellObj>().setData(data, mos);
        GameManager.player.GetComponent<Player>().attackEffect(obj);
    }


    public void setData(SpellData dt)
    {
        data = dt;
    }


}
