using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Choice : MonoBehaviorObj, IPointerEnterHandler, IPointerClickHandler
{
    public int index;
    [SerializeField]
    public Sprite select;
    public Sprite deSelect;
    public NPC npc;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerHandler(eventData);
    }

    public virtual void OnPointerHandler(PointerEventData eventData) 
    {
        // Mouse point Enter, showing select buttons
        npc.openmenu.GetComponent<NPCUI>().DeSelectHandler(npc.openmenu.GetComponent<NPCUI>().currentSelection);
        npc.openmenu.GetComponent<NPCUI>().currentSelection = index;
        npc.openmenu.GetComponent<NPCUI>().SelectHandler(index);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Work();
    }

    // Start is called before the first frame update
    public abstract void Work();
}
