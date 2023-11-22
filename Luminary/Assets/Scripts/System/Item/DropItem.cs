using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : InteractionTrriger
{
    public Item item;


    [SerializeField]
    SpriteRenderer spriteRenderer;

    public void Start()
    {
        interactDist = 2f;
        text = "�ݱ�";
    }


    public void setSpr()
    {
        spriteRenderer.sprite = item.data.itemImage;
    }

    public override void isInteraction()
    {
        if (GameManager.player.GetComponent<Charactor>().ItemAdd(item))
        {
            PlayerDataManager.interactionObject = null;

            base.isInteraction();
            GameManager.Resource.Destroy(popupUI);
            GameManager.Resource.Destroy(this.gameObject);
        }
    }
}
