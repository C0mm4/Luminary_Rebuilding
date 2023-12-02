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
        if (isFall)
        {
            transform.position += new Vector3(0, -10f, 10f) * Time.deltaTime;
            if (transform.position.z >= 0)
            {
                if(shadow != null)
                {
                    transform.position = shadow.transform.position;
                    isFall = false;
                    GetComponent<Rigidbody2D>().simulated = true;

                }
            }
        }
        if(isActivate)
        {
            transform.position += (shooter.transform.position - transform.position) * Time.deltaTime * 2f;
        }
    }
}
