using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equip : ItemSlot
{


    public override void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                GameManager.player.GetComponent<Player>().Unequip(index, item);
                GameManager.Instance.uiManager.invenFresh();
            }

        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        // 드래그를 끝낼 때 호출되는 함수
        if (GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex != -1)
        {
            if (eventData.pointerEnter != null)
            {
                Equip equip = eventData.pointerEnter.GetComponent<Equip>();
                if (equip != null)
                {
                    if (equip != null && equip != this)
                    {
                    }
                }
                else
                {
                    ItemSlot targetSlot = eventData.pointerEnter.GetComponent<ItemSlot>();

                    if (targetSlot != null && targetSlot != this)
                    {
                        GameManager.player.GetComponent<Player>().Unequip(index, item, targetSlot.index);
                    }
                }

            }
        }
        GameManager.Resource.Destroy(GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().tmpitem);
        GameManager.Instance.uiManager.invenFresh();
        GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex = -1;
        GameManager.inputManager.isDragging = false;
    }

}
