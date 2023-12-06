using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : InteractionTrriger
{
    public bool canUse = false;
    // Start is called before the first frame update
    void Start()
    {
        interactDist = 1f;
        text = "줍는다.";
    }

    public override void Update()
    {
        base.Update();
        if (GameManager.player.GetComponent<Player>().status.maxHP == GameManager.player.GetComponent<Player>().status.currentHP)
        {
            SetText("최대 체력입니다.");
            canUse = false;
        }
        else
        {
            SetText(PlayerDataManager.keySetting.InteractionKey + " - " + "줍는다.");
            canUse = true;

        }
    }

    public override void isInteraction()
    {
        if (canUse)
        {
            GameManager.player.GetComponent<Player>().HPIncrease(1);
            base.isInteraction();
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
