using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MobAttack
{
    [SerializeField]
    public RainShadow shadow;

    public bool isFall;
    public bool isActivate;
    // Start is called before the first frame update
    void Start()
    {
        isFall = true;
        isActivate = false;
        GetComponent<Rigidbody2D>().simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivate)
        {
            transform.position += (shooter.transform.position - transform.position) * Time.deltaTime * 2f;
        }
    }
}
