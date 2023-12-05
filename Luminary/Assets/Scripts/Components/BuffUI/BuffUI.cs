using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
    // Start is called before the first frame update
    float time;
    public Vector3 pos = new Vector3();
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<RectTransform>().localPosition = GameManager.cameraManager.camera.WorldToScreenPoint(pos) - new Vector3(GameManager.cameraManager.camera.pixelWidth / 2, GameManager.cameraManager.camera.pixelHeight / 2);
        if (Time.time - time >= 1f)
        {
            GameManager.Resource.Destroy(gameObject);
        }
    }
}
