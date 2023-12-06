using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : ItemSlot
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
        // �巡�׸� ���� �� ȣ��Ǵ� �Լ�
        if (GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex != -1)
        {
            if (eventData.pointerEnter != null)
            {
                WeaponSlot equip = eventData.pointerEnter.GetComponent<WeaponSlot>();
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
                        GameManager.player.GetComponent<Player>().Unequip(index, item);
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
