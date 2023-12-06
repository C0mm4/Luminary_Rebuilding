using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTultip : Menu
{
    public Vector2 pos;
    public Image img;
    public GameObject imgObj;
    public float width, height;

    public bool isRunning;

    public override void ConfirmAction()
    {

    }

    public override void InputAction()
    {
        if (Input.anyKeyDown)
        {
            if (isRunning)
            {
                img.rectTransform.sizeDelta = new Vector2(width, height);
            }
            else
            {
                Next();
            }
        }
    }

    public void setData(Vector2 pos, float width, float height)
    {
        this.pos = pos;
        this.width = width;
        this.height = height;
        isRunning = true;
        imgObj.transform.position = pos;
        img.rectTransform.sizeDelta = new Vector2(width + 2000, height + 2000);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if(width <= img.rectTransform.rect.width)
            {

                img.rectTransform.sizeDelta -= new Vector2(500,500) * Time.deltaTime;
            }
            else
            {
                img.rectTransform.sizeDelta = new Vector2(width, height);
                isRunning = false;
            }
        }
    }

    public void Next()
    {
        exit();

    }
}
