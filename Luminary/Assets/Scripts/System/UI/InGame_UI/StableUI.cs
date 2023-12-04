using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StableUI : MonoBehaviorObj
{
    // HP MP Bar Sprites
    [SerializeField]
    Sprite BlankHP_L;
    [SerializeField]
    Sprite BlankHP_C;
    [SerializeField] 
    Sprite BlankHP_R;
    [SerializeField]
    Sprite BlankMP_L;
    [SerializeField]
    Sprite BlankMP_C;
    [SerializeField]
    Sprite BlankMP_R;

    [SerializeField]
    GameObject HP;
    [SerializeField]
    GameObject Mana;
    
    [SerializeField]
    Sprite FillHP_L;
    [SerializeField]
    Sprite FillHP_C;
    [SerializeField]
    Sprite FillHP_R;
    [SerializeField]
    Sprite FillMana_L;
    [SerializeField]
    Sprite FillMana_C;
    [SerializeField]
    Sprite FillMana_R;

    [SerializeField]
    SpriteRenderer FillHPBar;
    [SerializeField]
    SpriteRenderer FillManaBar;

    public List<GameObject> HPBar;
    public List<GameObject> currentHPBar;
    public List<GameObject> MPBar;
    public List<GameObject> currentMPBar;

    public List<GameObject> weaponSlot;

    public bool isCast = false;
    public GameObject castBar;
    public Image castFillin;
    public float castT;
    public float castStartT;

    [SerializeField]
    int maxHP, maxMana;
    [SerializeField]
    int currentHP, currentMana;
    [SerializeField]
    Player player;

    RectTransform rt;

    public void init()
    {
        // When Player Gen, Call Init();
        Debug.Log("init");
        player = GameManager.player.GetComponent<Player>();
        FreshMaxHPMP();
        castBar.SetActive(false);
    }



    public void Update()
    {
        if(GameManager.gameState == GameState.InPlay)
        {
            // Freshing HP, MP Bar
            FreshMaxHPMP();

            // Casting Bar UI
            if (isCast)
            {
                castBar.SetActive(true);
                castFillin.fillAmount = (Time.time - castStartT) / castT;
            }
            else
            {
                castBar.SetActive(false);
            }
        }
    }
    // Set Casting Time
    public void setCast(float castT, float startT)
    {
        this.castT = castT;
        this.castStartT = startT;
    }

    // Change showing weapon slot
    public void WeaponSlotChange(int n)
    {
        int disabletarget;
        if(n == 0)
        {
            disabletarget = 1;

        }
        else
        {
            disabletarget = 0;
        }

        weaponSlot[disabletarget].GetComponent<WeaponSlotUI>().disable();
        weaponSlot[n].GetComponent<WeaponSlotUI>().enable();
    }

    public void FreshMaxHPMP()
    {
        if(maxHP != GameManager.player.GetComponent<Player>().status.maxHP || currentHP != GameManager.player.GetComponent<Player>().status.currentHP)
        {
            maxHP = GameManager.player.GetComponent<Player>().status.maxHP;
            foreach (GameObject hp in HPBar)
            {
                GameManager.Resource.Destroy(hp);

            }
            HPBar.Clear();
            for(int i = 0; i < maxHP; i++)
            {
                GameObject go = new GameObject();
                go.AddComponent<RectTransform>();
                go.transform.SetParent(HP.transform, false);
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = new Vector3(i * 0.64f, 0, 2);
                go.AddComponent<SpriteRenderer>();
                if (i == 0)
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankHP_L;
                    Debug.Log("Left");
                }
                else if(i == maxHP - 1)
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankHP_R;
                }
                else
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankHP_C;
                }
                HPBar.Add(go);
            }

            currentHP = GameManager.player.GetComponent<Player>().status.currentHP;
            foreach(GameObject hp in currentHPBar)
            {
                GameManager.Resource.Destroy(hp);
            }
            currentHPBar.Clear();
            for(int i = 0; i < currentHP; i++)
            {
                GameObject go = new GameObject();
                go.AddComponent<RectTransform>();
                go.transform.SetParent(HP.transform, false);
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = new Vector3(i * 0.64f, 0, 0);
                go.AddComponent<SpriteRenderer>();
                if (i == 0)
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillHP_L;
                }
                else if (i == maxHP - 1)
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillHP_R;
                }
                else
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillHP_C;
                }
                currentHPBar.Add(go);
            }
        }
        if(maxMana != GameManager.player.GetComponent<Player>().status.maxMana || currentMana != GameManager.player.GetComponent<Player>().status.currentMana)
        {
            maxMana = GameManager.player.GetComponent<Player>().status.maxMana;
            foreach (GameObject hp in MPBar)
            {
                GameManager.Resource.Destroy(hp);

            }
            MPBar.Clear();
            for (int i = 0; i < maxMana; i++)
            {
                GameObject go = new GameObject();
                go.AddComponent<RectTransform>();
                go.transform.SetParent(Mana.transform, false);
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = new Vector3(i * 0.64f, 0, 2);
                go.AddComponent<SpriteRenderer>();
                if (i == 0)
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankMP_L;
                }
                else if (i == maxMana - 1)
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankMP_R;
                }
                else
                {
                    go.GetComponent<SpriteRenderer>().sprite = BlankMP_C;
                }
            }
            currentMana = GameManager.player.GetComponent<Player>().status.currentMana;
            foreach (GameObject hp in currentMPBar)
            {
                GameManager.Resource.Destroy(hp);

            }
            currentMPBar.Clear();
            for (int i = 0; i < currentMana; i++)
            {
                GameObject go = new GameObject();
                go.AddComponent<RectTransform>();
                go.transform.SetParent(Mana.transform, false);
                go.GetComponent<RectTransform>().localScale = Vector3.one;
                go.GetComponent<RectTransform>().localPosition = new Vector3(i * 0.64f, 0, 0);
                go.AddComponent<SpriteRenderer>();
                if (i == 0)
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillMana_L;
                }
                else if (i == maxMana - 1)
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillMana_R;
                }
                else
                {
                    go.GetComponent<SpriteRenderer>().sprite = FillMana_C;
                }
                currentMPBar.Add(go);
            }

        }
    }

}
