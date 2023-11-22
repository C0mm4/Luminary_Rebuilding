using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private Image image;

    [SerializeField]
    public int index;

    [SerializeField]
    private Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.data.itemImage;
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void Awake()
    {
        image.color = new Color(1, 1, 1, 0);
    }


    public void OnDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            if (!GameManager.inputManager.isDragging)
            {

                GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex = index;
                GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().tmpitem = GameManager.Resource.Instantiate("UI/TmpItem");
                GameObject tmpobj = GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().tmpitem;
                tmpobj.GetComponent<SpriteRenderer>().sprite = image.sprite;
                tmpobj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
                tmpobj.transform.position = new Vector3(GameManager.inputManager.mouseWorldPos.x, GameManager.inputManager.mouseWorldPos.y, -1);
                tmpobj.transform.localScale = new Vector2(2.5f, 2.5f);

                GameManager.inputManager.isDragging = true;
            }
            
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        // �巡�׸� ���� �� ȣ��Ǵ� �Լ�
        if (GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex != -1)
        {
            if (eventData.pointerEnter != null)
            {
                Equip equip = eventData.pointerEnter.GetComponent<Equip>();
                if(equip != null)
                {
                    if(equip != null && equip != this)
                    {
                        if(item.data.type == 1)
                        GameManager.player.GetComponent<Player>().Equip(index, GameManager.player.GetComponent<Player>().status.inventory[index].item, equip.index);

                    }
                }
                else
                {
                    WeaponSlot weaponslot = eventData.pointerEnter.GetComponent<WeaponSlot>();
                    if(weaponslot != null)
                    {
                        if(weaponslot != this)
                        {
                            if(item.data.type == 0)
                            GameManager.player.GetComponent<Player>().Equip(index, GameManager.player.GetComponent<Player>().status.inventory[index].item, weaponslot.index);
                        }
                    }
                    else
                    {
                        ItemSlot targetSlot = eventData.pointerEnter.GetComponent<ItemSlot>();

                        if (targetSlot != null && targetSlot != this)
                        {
                            GameManager.player.GetComponent<Player>().ItemSwap(index, targetSlot.index);
                        }

                    }
                }

            }
        }
        GameManager.Resource.Destroy(GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().tmpitem);
        GameManager.Instance.uiManager.invenFresh();
        GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().clickIndex = -1;
        GameManager.inputManager.isDragging = false;
    }


    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click Detected by index : " + index);
        if(item != null)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                GameManager.player.GetComponent<Player>().Equip(index, item);
                GameManager.Instance.uiManager.invenFresh();
            }

        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        {
            GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().hoveringUI = GameManager.Resource.Instantiate("UI/ItemHoveringUI");
            GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().hoveringUI.GetComponent<ItemHoveringUI>().setData(item);
            GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().hoveringUI.GetComponent<RectTransform>().localPosition = GameManager.cameraManager.camera.WorldToScreenPoint(GameManager.inputManager.mouseWorldPos) - new Vector3(Screen.width / 2, Screen.height / 2, 0) + new Vector3(260, 0, 0);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Resource.Destroy(GameManager.Instance.uiManager.invUI.GetComponent<Inventory>().hoveringUI);
    }
}
