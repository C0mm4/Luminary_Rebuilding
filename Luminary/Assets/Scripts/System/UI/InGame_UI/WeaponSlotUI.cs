using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviorObj
{
    RectTransform rect;
    [SerializeField]
    SpriteRenderer WeaponSpr;

    [SerializeField]
    Image SpellSpr;
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
        SpellSpr.color = new Color(1, 1, 1, 1);

    }

    public void deSetWeapon(int i)
    {
        WeaponSpr.sprite = GameManager.Resource.LoadSprite("default");
        SpellSpr.sprite = null;
        SpellSpr.color = new Color(1, 1, 1, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

}
