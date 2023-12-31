using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownFall : MonoBehaviour
{
    public bool isFallEnd;
    public Vector3 originPos;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.z >= 0)
        {
            if (!isFallEnd)
            {
                transform.position = originPos;
                GetComponent<Rigidbody2D>().simulated = true;
                isFallEnd = true;

            }
        }
        else
        {
            transform.position += new Vector3(0, -10, 10) * Time.deltaTime;
            GetComponent<Rigidbody2D>().simulated = false;
            isFallEnd = false;
        }
    }

    private void Start()
    {
        originPos = transform.position;
        GetComponent<Rigidbody2D>().simulated = false;
        transform.position += new Vector3(0f, 22f, -22f);
        isFallEnd = false;
    }
}
