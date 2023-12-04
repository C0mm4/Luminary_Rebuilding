using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatturn4 : Patturn
{
    bool isRoomDataSet;
    List<GameObject> pillars = new List<GameObject>();
    public int pillarsN = -1;
    public override void Update()
    {
        if (issetData)
        {
            if (!isRoomDataSet)
            {
                isRoomDataSet = true;
                DunRoom currentRoom = GameManager.StageC.rooms[GameManager.StageC.currentRoom];
                mob.transform.position = currentRoom.transform.position;

            }
            else if(!isActivate)
            {
                isActivate = true;
                StartCoroutine(Action());
            }
            else
            {
                if (pillarsN == 0)
                {
                    mob.changeState(new MobIdleState());
                    GameManager.Resource.Destroy(gameObject);
                }
                else
                {
                    List<GameObject> delList = new List<GameObject>();
                    foreach(GameObject go in pillars)
                    {
                        try
                        {
                            go.GetComponent<Mob>();
                        }
                        catch
                        {
                            delList.Add(go);
                        }
                    }
                    foreach(GameObject go in delList)
                    {
                        int i = pillars.FindIndex(item => item.GetInstanceID() == go.GetInstanceID());
                        pillars.RemoveAt(i);
                        pillarsN--;
                    }
                }
            }
        }
    }

    public IEnumerator Action()
    {
        yield return new WaitForSeconds(1f);

        GameObject go = GameManager.Resource.Instantiate("Mobs/Shadows", mob.transform.position);
        go.transform.localScale = new Vector3(5, 5, 1);

        yield return new WaitForSeconds(1f);
        mob.AnimationPlay("AttackAnimation 3", 0.7f);

        yield return new WaitForSeconds(1f);
        GameManager.Resource.Destroy(go);

        pillars = new List<GameObject>();
        transform.position = mob.transform.position;
        for(int i = 0; i < 4; i++)
        {
            go = GameManager.Resource.Instantiate("Mobs/Slime/AttackPrefabs/Pillar", transform);
            go.transform.position = GameManager.StageC.rooms[GameManager.StageC.currentRoom].transform.position + new Vector3(Func.xXpos[i] * 5.25f, Func.yXpos[i] * 4f + -2);
            pillars.Add(go);
            go.GetComponent<Mob>().spawnActive = true;
            GameManager.StageC.rooms[GameManager.StageC.currentRoom].mobCount++;
        }

        pillarsN = 4;

    }

}
