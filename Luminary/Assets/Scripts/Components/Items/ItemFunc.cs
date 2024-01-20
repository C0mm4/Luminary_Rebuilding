using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemFunc
{
    // Item Data
    public ItemData data;
    // Item Equip Effect
    public abstract void EquipEffect();
    // Item UnEquip Effect
    public abstract void UnEquipEffect();
    // Item Effect on game Frame
    public abstract void OnFrameEffect();
    // Item Effect when Hit enemy
    public abstract void OnHitEffect(GameObject spellObj);
    // Item Effect when Damaged
    public abstract void OnDamagedEffect();
    
}
