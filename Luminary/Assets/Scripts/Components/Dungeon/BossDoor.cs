using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : InteractionTrriger
{
    public bool isActivate = false;

    public void Start()
    {
        text = "입장한다";
    }

    public override void Update()
    {
        if (!isActivate)
        {
            base.Update();
        }
    }

    public override void isInteraction()
    {
        GameManager.gameState = GameState.Loading;
        StartCoroutine(Action());
    }

    public IEnumerator Action()
    {
        yield return 0;
        GetComponent<Rigidbody2D>().simulated = false;
        for(int i = 0; i < 20; i++)
        {
            if(i % 2 == 0)
            {
                if(i == 0)
                {
                    transform.position += new Vector3(-0.1f, 0.1f);
                }
                else
                {

                    transform.position += new Vector3(-0.2f, 0.1f);
                }
            }
            else
            {
                if(i == 19)
                {
                    transform.position += new Vector3(0.1f, 0.1f);
                }
                else
                {
                    transform.position += new Vector3(0.2f, 0.1f);
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
        GameManager.Resource.Destroy(gameObject);
        GameManager.gameState = GameState.InPlay;
    }
}