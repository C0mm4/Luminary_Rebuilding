using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob : Charactor
{
    // Mob Attack Prefab
    [SerializeField]
    public GameObject[] attackPrefab;
    public bool[] isHitbox;
    public MobData data;
    public AIModel model;
    public List<float> lastAttackT = new List<float>();

    public List<MobAttack> AtkObj;
    public MobAttack ActiveAtk;

    public Vector2 sawDirect;

    public bool isCollider;

    public bool spawnActive;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        // Set Idle State
        sMachine.changeState(new MobIdleState());
        AtkObj = new List<MobAttack>();
        // Find Player
        try
        {
            player = GameObject.Find("PlayerbleChara").GetComponent<Charactor>();
        }
        catch
        {
            sMachine.changeState(new MobIdleState());
        }
        isCollider = true;
        AIGen();
        spawnActive = false;
    }


    // AI Generate by string name
    public void AIGen()
    {
        Type T = Type.GetType(data.AI);
        model = Activator.CreateInstance(T) as AIModel;
        model.target = this;
        Debug.Log(model.GetType().Name);
    }

    // status initialize based on data
    public override void statusInit()
    {
        status.baseHP = data.baseHP;
        status.baseDMG = data.baseDMG;
        status.basespeed = data.basespeed;
        for(int i = 0; i < data.castSpeed.Count; i++)
        {
            lastAttackT.Add(0f);
        }
        base.statusInit();
    }


    public override void Update()
    {
        if (spawnActive)
        {

            // if Player didn't find, research player object
            if (player == null)
            {
                try
                {
                    player = GameObject.Find("PlayerbleChara").GetComponent<Charactor>();
                }
                catch
                {
                    setIdleState();
                }
            }

            // AI model update
            model.Update();


            base.Update();
        }
    }

    public void OnDestroy()
    {
        int i = GameManager.Random.getGeneralNext(0, 100);
        Debug.Log(i);
        if(i < 10)
        {
            GameObject go = GameManager.Resource.Instantiate("Obj/Heart");
            go.transform.position = transform.position;
        }
    }

    public override void DieObject()
    {
        // Current Room's mob count decrease
        GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<DunRoom>().mobCount -= 1;
        if(GameManager.StageC.rooms[GameManager.StageC.currentRoom].GetComponent<DunRoom>().mobCount == 0)
        {
            GameManager.StageC.ClearRoom();
        }
        AtkObj.Clear();
        GameManager.player.GetComponent<Player>().status.gold += data.dropGold;
        model.DestroyHandler();
        base.DieObject();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollider)
        {
            if (other.transform.tag == "Player")
            {

                other.gameObject.GetComponent<Charactor>().HPDecrease(1);
            }

        }
    }

    // return player Distance on Vector2
    public Vector2 playerDistance()
    {

        Vector2 ret = new Vector2(Math.Abs(player.transform.position.x - transform.position.x), Math.Abs(player.transform.position.y - transform.position.y));
        return ret;
    }

    // return player Direction on Vector2
    public Vector2 playerDir()
    {
        Vector2 ret = new Vector2((player.transform.position.x - transform.position.x), (player.transform.position.y - transform.position.y));
        return ret;
    }
    
    // Attack Object Generate
    public void AttackObjGen(int index)
    {
        // if HitBox Attack
        if (isHitbox[index])
        {
            GameObject go = GameManager.Resource.Instantiate(attackPrefab[index], transform);
            ActiveAtk = go.GetComponent<MobAttack>();
            ActiveAtk.setData(this);
        }
        else
        {
            GameObject go = GameManager.Resource.Instantiate(attackPrefab[index]);
            ActiveAtk = go.GetComponent<MobAttack>();
            try
            {
                // if ProjecTile Attack
                ActiveAtk.GetComponent<MobProjectile>().setData(this);
                Debug.Log("Projectile");
            }
            catch
            {
                // if Field Attack
                try
                {
                    bool isrnd = ActiveAtk.GetComponent<MobField>().isRandom;
                    if (isrnd)
                    {
                        Vector3 pos = new Vector3();
                        ActiveAtk.GetComponent<MobField>().setData(this, pos);
                    }
                    else
                    {
                        ActiveAtk.GetComponent<MobField>().setData(this);
                    }
                    Debug.Log("Field");
                }
                catch 
                {
                    // if Control Object Attack
                    go.GetComponent<Patturn>().setData(this);
                    go.transform.position = new Vector3(transform.position.x, transform.position.z, 1);
                }
            }
        }
        if(ActiveAtk != null)
            AtkObj.Add(ActiveAtk);
    }

    public void AttackActivates()
    {
        try
        {
            ActiveAtk.Activate();
            ActiveAtk = null;
        }
        catch
        {

        }
    }

    // Attack State End handler Control in Animation Event
    public void attakEnd(int n)
    {
        lastAttackT[n] = Time.time;
        if(getState().GetType().Name == "MobATKState")
        {
            if (isHitbox[n])
            {
                if(ActiveAtk != null)
                    GameManager.Resource.Destroy(ActiveAtk.gameObject);
            }
            endCurrentState();

        }
    }

    public float HPPercent()
    {
        return (float)((float)status.currentHP / (float)status.maxHP);
    }

    public void MoveSetPosition(float x, float y)
    {
        Vector3 Roompos = GameManager.StageC.rooms[GameManager.StageC.currentRoom].transform.position;
        Vector3 pos = new Vector3(Roompos.x + x, Roompos.y + y);
        transform.position = pos;
    }

    public void MovePosition(float x, float y)
    {
        transform.position += new Vector3(x, y);
    }

    public void SetHitBox()
    {
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<CircleCollider2D>().isTrigger = false;
    }

    public void DeSetHitBox()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    public void ColliderOn()
    {
        GetComponent<Rigidbody2D>().simulated = true;
    }
    
    public void ColliderOff()
    {
        GetComponent<Rigidbody2D>().simulated = false;
    }

    public void ColliderMove(float y)
    {
        GetComponent<CapsuleCollider2D>().offset += new Vector2(0, y);
    }

    public void StateNull()
    {
        setIdleState();
        sMachine.changeState(null);
        Debug.Log("null");
    }
}
