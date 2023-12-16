using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : InteractionTrriger
{
    // Set Interect Distance, and Overlay Text
    public void Start()
    {
        interactDist = 3f;
        text = "이동한다";
    }

    // Interaction Trigger is Activate
    public override void isInteraction()
    {
        Func.SetRectTransform(GameManager.Resource.Instantiate("UI/EndScreen"));
        base.isInteraction();
    }
}
