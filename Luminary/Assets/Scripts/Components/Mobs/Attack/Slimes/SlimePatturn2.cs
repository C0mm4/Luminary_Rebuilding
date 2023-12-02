using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatturn2 : Patturn
{
    Vector3 v1, v2;
    bool isRoomDataSet;
    List<GameObject> shadows;
    List<GameObject> jellys;

    public override void Update()
    {
        if (issetData)
        {
            if (!isRoomDataSet)
            {
                DunRoom currentRoom = GameManager.StageC.rooms[GameManager.StageC.currentRoom];
                mob.transform.position = currentRoom.gameObject.transform.position;
                (v1, v2) = currentRoom.roomRange();
                isRoomDataSet = true;
            }
            else if (!isActivate)
            {
                StartCoroutine(Action());
                isActivate = true;
            }
        }
    }

    public IEnumerator Action()
    {
        yield return new WaitForSeconds(1f);
        jellys = new List<GameObject>();
        shadows = new List<GameObject>();
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                yield return new WaitForSeconds(0.15f);
                GameObject go = GameManager.Resource.Instantiate("Mobs/Slime/AttackPrefabs/RainShadow",transform);
                GameObject go2 = GameManager.Resource.Instantiate("Mobs/Slime/AttackPrefabs/Rain", transform);
                float rX = (float) GameManager.Random.getGeneralNext(v1.x, v2.x);
                float rY = (float)GameManager.Random.getGeneralNext(v1.y, v2.y);
                while (isNear(rX, rY))
                {
                    rX = (float)GameManager.Random.getGeneralNext(v1.x, v2.x);
                    rY = (float)GameManager.Random.getGeneralNext(v1.y, v2.y);
                }
                go.transform.position = new Vector3(rX, rY, 0);
                go2.transform.position = go.transform.position + new Vector3(0, 22, -22);
                go2.GetComponent<Rain>().shadow = go.GetComponent<RainShadow>();
                go2.GetComponent<MobAttack>().setData(mob);
                shadows.Add(go);
                jellys.Add(go2);
            }

            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(3f);
        foreach(GameObject go in shadows)
        {
            GameManager.Resource.Destroy(go);
        }
        shadows.Clear();

        foreach(GameObject go in jellys)
        {
            go.GetComponent<Rain>().isActivate = true;
        }

        yield return new WaitForSeconds(1f); 
        foreach (GameObject go in jellys)
        {
            GameManager.Resource.Destroy(go);
        }
        mob.AnimationPlay("MergeFirst");

        yield return new WaitForSeconds(3f);
        mob.AnimationPlay("MergeFinal");

        yield return new WaitForSeconds(1f);
        mob.lastAttackT[1] = Time.time;
        mob.changeState(new MobIdleState());
        GameManager.Resource.Destroy(gameObject);
    }

    public bool isNear(float x, float y)
    {
        foreach(GameObject go in shadows)
        {
            Vector2 pos = go.transform.position;
            float distance = Mathf.Sqrt(Mathf.Pow(pos.x - x, 2) + Mathf.Pow(pos.y - y, 2));
            if (distance <= 0.25f)
                return true;
        }
        return false;
    }
}
