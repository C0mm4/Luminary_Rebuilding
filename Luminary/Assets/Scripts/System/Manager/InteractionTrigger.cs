using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class InteractionTrriger : MonoBehaviorObj
{
    private float distanceToPlayer;
    [SerializeField]
    public float interactDist;

    public Transform UIPosition;
    [SerializeField]
    protected GameObject popupUI;

    [SerializeField]
    protected string text;

    

    // Update is called once per frame
    public virtual void  Update()
    {
        if((GameManager.uiState == UIState.InPlay || GameManager.uiState == UIState.Lobby) && GameManager.gameState == GameState.InPlay)
        {
            // Find Nearby Objects to Player Object
            if (GameObject.FindWithTag("Player"))
            {
                distanceToPlayer = Vector3.Distance(transform.position, GameManager.player.transform.position);
                if (PlayerDataManager.interactionObject != gameObject)
                {
                    if (distanceToPlayer <= interactDist && distanceToPlayer <= PlayerDataManager.interactionDistance)
                    {
                        PlayerDataManager.interactionObject = gameObject;
                        PlayerDataManager.interactionDistance = distanceToPlayer;
                        // ac
                    }
                }
                else
                {
                    if (distanceToPlayer > interactDist)
                    {
                        PlayerDataManager.interactionObject = null;
                        PlayerDataManager.interactionDistance = interactDist + 1f;
                        //ac
                    }
                }
            }
            // if This Object is nearby objects to player, Interaction Hovering UI generate
            if (PlayerDataManager.interactionObject == gameObject)
            {
                if (popupUI == null)
                {
                    GetComponent<Trans2Canvas>().GenerateUI("UI/InteractionUI");
                    popupUI = GetComponent<Trans2Canvas>().UIObj;


//                    popupUI = GameManager.Resource.Instantiate("UI/InteractionUI", GameManager.Instance.canvas.transform);
                    popupUI.GetComponent<InteractHover>().textorigin = PlayerDataManager.keySetting.InteractionKey + " - " + text;

                }
                PopUpMenu();
            }
            else
            {
                if(popupUI != null)
                {
                    popupUI.GetComponent<InteractHover>().Close();
                    popupUI = null;
                }
            }
        }
        else
        {
            if(popupUI != null)
            {
                GameManager.Resource.Destroy(popupUI.gameObject);
                popupUI = null;
            }
        }
    }

    // Interaction Trigger function
    public virtual void isInteraction()
    {
        PlayerDataManager.interactionObject = null;
        PlayerDataManager.interactionDistance = 5.5f;
        GameManager.Resource.Destroy(popupUI.gameObject);
    }

    // Set Interaction Hovering UI Position
    public void PopUpMenu()
    {
//        Func.SetRectTransform(popupUI, GameManager.cameraManager.camera.WorldToScreenPoint(GetComponent<Trans2Canvas>().UIIngameTransform.position));
    }
}
