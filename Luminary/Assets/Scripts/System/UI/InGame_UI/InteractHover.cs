using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractHover : MonoBehaviorObj
{
    [SerializeField]
    public TMP_Text text;
    public string textorigin;
    public Image img;
    public int textCnt;
    public float fillin;

    public bool close;

    public void Start() 
    {
        StartCoroutine(Gen());
    }

    public IEnumerator Gen()
    {
        for(fillin = 0; fillin < 1.05f; fillin += 0.1f)
        {
            if (close)
            {
                yield break;
            }
            img.fillAmount = fillin;
            yield return new WaitForSeconds(0.05f);
        }
        for(textCnt = 0; textCnt < textorigin.Length; textCnt++)
        {
            if (close)
            {
                yield break;
            }
            text.text += textorigin[textCnt];
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Close()
    {
        StartCoroutine(CloseAction());
        close = true;
    }

    public IEnumerator CloseAction() 
    {
        for (int i = textCnt - 1; i >= 0; i--)
        {
            text.text = textorigin[..i];
            yield return new WaitForSeconds(0.05f);
        }
        for (float i = fillin; i >= 0; i -= 0.1f)
        {
            img.fillAmount = i;
            yield return new WaitForSeconds(0.05f);
        }
        GameManager.Resource.Destroy(gameObject);
    }
}
