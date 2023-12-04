using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIModel
{
    // target Mob
    public Mob target;

    
    public abstract void Update();
    public virtual void DestroyHandler()
    {

    }
}
