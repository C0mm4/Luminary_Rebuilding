using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatturn4 : Patturn
{
    bool isRoomDataSet;
    List<GameObject> pillars;
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
        }
    }

    public IEnumerator Action()
    {
        yield return new WaitForSeconds(1f);
        pillars = new List<GameObject>();
        for(int i = 0; i < 4; i++)
        {
            GameObject go = GameManager.Resource.Instantiate("", transform);
            go.transform.position = Vector3.zero;
            pillars.Add(go);
        }

        
    }

}
