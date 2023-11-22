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
        UIGen();
    }

    public void UIGen()
    {
        GameObject go = GameManager.Resource.Instantiate("UI/BossUI/BossUI");
        Func.SetRectTransform(go);
        BossUI ui = go.GetComponent<BossUI>();
        ui.boss = this;
        ui.SetData();
    }

    public override void FixedUpdate()
    {
        if (isSpawnAction)
        {
            // Spawn Scene Play
            SpawnSceneStart();
            base.FixedUpdate();
        }
        else
        {
            base.FixedUpdate();
        }
    }

    public void SpawnSceneEnd()
    {
        isSpawnAction = false;
        GameManager.Instance.uiManager.ChangeState(UIState.InPlay);
        GameManager.cameraManager.player = GameManager.player.transform;
    }

    public void SpawnSceneStart()
    {
        GameManager.cameraManager.player = transform;
        AnimationPlay("SpawnAnimation");
        GameManager.Instance.uiManager.ChangeState(UIState.CutScene);
    }
}
