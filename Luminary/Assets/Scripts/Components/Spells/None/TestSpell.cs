using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class TestSpell : SpellObj 
{

    public override void Start()
    {
        base.Start();
        run();
    }

    
    public void run()
    {
        // Accelerated Player Speed
        State getspell = player.GetComponent<Charactor>().getState();
        if (getspell.GetType().Name == "PlayerMoveState")
        {
//            player.GetComponent<Charactor>().changeState(new PlayerRollState(mos));
            // After 0.3f seconds rollback Player Speed
            Invoke("endrun", 0.2f);
        }
        else
        {
            GameManager.Resource.Destroy(this.gameObject);
        }
    }
    
    public void endrun()
    {
        player.GetComponent<Charactor>().endCurrentState();
        GameManager.Resource.Destroy(this.gameObject);
    }

}