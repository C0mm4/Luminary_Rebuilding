using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainShadow : MonoBehaviorObj
{
    public void Start()
    {
        GetComponent<Rigidbody2D>().simulated = false;
    }
}
