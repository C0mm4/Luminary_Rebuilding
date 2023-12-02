using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatturn4 : Patturn
{
    bool isRoomDataSet;
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
        yield return 0;

    }

}
