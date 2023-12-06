using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(
            GameManager.inputManager.mouseWorldPos.x, 
            GameManager.inputManager.mouseWorldPos.y, -15);
    }
}
