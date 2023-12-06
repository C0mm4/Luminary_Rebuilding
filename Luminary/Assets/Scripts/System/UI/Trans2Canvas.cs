using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trans2Canvas : MonoBehaviour
{
    public GameObject UIPrefab;
    public GameObject UIIngameTransform;

    public GameObject UIObj;

    
    public void GenerateUI(string prefabPath)
    {
        UIObj = GameManager.Resource.Instantiate(UIPrefab, GameManager.Instance.canvas.transform);
        Func.SetRectTransform(UIObj, GameManager.cameraManager.camera.WorldToScreenPoint(UIIngameTransform.transform.position) - new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f));
    }

    public void Update()
    {
        if(UIObj != null)
        {
            Func.SetRectTransform(UIObj, GameManager.cameraManager.camera.WorldToScreenPoint(UIIngameTransform.transform.position) - new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f));
        }
    }
}
