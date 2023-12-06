using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Mob
{
    public bool isSpawnAction;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        
        isboss = true;
        isSpawnAction = true;
        Debug.Log(sMachine.getStateStr());
        spawnActive = true;
    }

    public void UIGen()
    {
        GameObject go = GameManager.Resource.Instantiate("UI/BossUI/BossUI");
        Func.SetRectTransform(go);
        BossUI ui = go.GetComponent<BossUI>();
        ui.boss = this;
        ui.SetData();
    }

    public override void Update()
    {
        if (isSpawnAction)
        {
            // Spawn Scene Play
            SpawnSceneStart();
        }
        else
        {
            base.Update();
        }
    }

    public void SpawnSceneEnd()
    {
        UIGen();
        StartCoroutine(spawnSceneEndTrigger());
    }

    public void SpawnSceneStart()
    {
        GameManager.cameraManager.player = transform;
        AnimationPlay("SpawnCutScene");
        GameManager.Instance.uiManager.ChangeState(UIState.CutScene);
    }

    public override void DieObject()
    {
        DunRoom currentRoom = GameManager.StageC.rooms[GameManager.StageC.currentRoom];
        GameObject go = GameManager.Resource.Instantiate("Dungeon/Portal/EndPortal", currentRoom.gameObject.transform.position);
        go.transform.position = currentRoom.transform.position;
        
        base.DieObject();
    }

    public void OnDestroy()
    {
        GameManager.Resource.Destroy(GameObject.Find("BossUI (Clone)"));
    }

    public IEnumerator spawnSceneEndTrigger()
    {
        yield return new WaitForSeconds(3f);
        changeState(new MobIdleState());
        GameManager.Instance.uiManager.ChangeState(UIState.InPlay);
        GameManager.cameraManager.player = GameManager.player.transform;
        Debug.Log("End");
        isSpawnAction = false;
    }

}
