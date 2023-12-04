using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnner : MonoBehaviour
{
    public List<GameObject> mobLists = new List<GameObject>();

    public Dictionary<int, GameObject> mobDict = new Dictionary<int, GameObject>();

    // Set Mob Dictionary
    public void init()
    {
        foreach(GameObject mob in mobLists)
        {
            mobDict[mob.GetComponent<Mob>().data.index] = mob;
        }
    }

    // spawn mobs by index
    public GameObject spawnMob(int index, Transform transform, Transform parent = null)
    {
        GameObject go;
        if(parent == null)
        {
            go = GameManager.Resource.Instantiate(mobLists[index]);
            go.transform.position = transform.position;
        }
        else
        {
            go = GameManager.Resource.Instantiate(mobLists[index], parent);
            go.transform.position = transform.position;
        }
        if (go != null) 
        {
            return go;
        }
        else
        {
            return null;
        }
    }
}
