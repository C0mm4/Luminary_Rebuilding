using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPortal_InGame : InteractionTrriger
{
    // Set Interect Distance, and Overlay Text
    public void Start()
    {
        interactDist = 5f;
        text = "�̵��Ѵ�";
    }

    // Interaction Trigger is Activate
    public override void isInteraction()
    {
        GameManager.Instance.sceneControl("StageScene");
        base.isInteraction();
    }
}
