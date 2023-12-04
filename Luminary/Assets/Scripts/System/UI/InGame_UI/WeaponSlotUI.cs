using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotUI : MonoBehaviorObj
{
    RectTransform rect;
    [SerializeField]
    SpriteRenderer WeaponSpr;

    [SerializeField]
    SpriteRenderer SpellSpr;
    public void enable()
    {
        gameObject.SetActive(true);
        
    }

    public void disable()
    {
        gameObject.SetActive(false);
    }

    public void setWeapon(Item item)
    {
        WeaponSpr.sprite = item.data.itemImage;
        SpellSpr.sprite = GameManager.Spells.getSpellData(item.data.spellnum).spr;
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

}
