using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI : Menu
{
    public override void ConfirmAction()
    {

    }

    public override void InputAction()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.sceneControl("LobbyScene");
            exit();
        }
    }

}
