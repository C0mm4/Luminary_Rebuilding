using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class FSMManager
{
    public Dictionary<string, List<string>> fsm;
    
    public void init()
    {
        fsm = new Dictionary<string, List<string>>();
        List<string> playerstate = new List<string>
        {
            "PlayerIdleState",
            "PlayerMoveState",
            "PlayerTeleportState",
            "PlayerCastingState",
            "DieState",
            "PlayerHitState",
            "PlayerMoveAbsolState",
            "PlayerStunState",
        };
        // Idle State FSM
        List<string> playerfsm = new List<string>
        {
            playerstate[1], playerstate[2], playerstate[3], playerstate[4], playerstate[5], playerstate[6], playerstate[7],
        };
        fsm[playerstate[0]] = playerfsm;

        // Move State FSM
        playerfsm = new List<string>
        {
            playerstate[1], playerstate[2], playerstate[3], playerstate[4], playerstate[5], playerstate[6], playerstate[7],
        };
        fsm[playerstate[1]] = playerfsm;

        // Roll State FSM
        playerfsm = new List<string>();
        fsm[playerstate[2]] = playerfsm;

        // Casting State FSM
        fsm[playerstate[3]] = playerfsm;
        // Die State FSM
        fsm[playerstate[4]] = playerfsm;
        // Hit State FSM
        fsm[playerstate[5]] = playerfsm;
        // Absolute Move FSM
        fsm[playerstate[6]] = playerfsm;

        fsm[playerstate[7]] = playerfsm;

        List<string> mobState = new List<string>
        {
            "MobIdleState",
            "MobChaseState",
            "MobRunState",
            "MobCastState",
            "MobATKState",
            "MobHitState",
            "DieState",
            "MobMoveState",
            "MobStunState",
        };

        // Mob Idle State FSM
        List<string> mobFSM = new List<string>
        {
            mobState[1], mobState[2], mobState[3], mobState[4], mobState[5], mobState[6], mobState[7], mobState[8], 
        };
        fsm[mobState[0]] = mobFSM;

        // Mob Chase State FSM
        mobFSM = new List<string>()
        {
            mobState[2], mobState[3], mobState[4], mobState[5], mobState[6], mobState[8],
        };
        fsm[mobState[1]] = mobFSM;

        // Mob Run State FSM
        mobFSM = new List<string>()
        {
            mobState[3], mobState[4], mobState[5], mobState[6], mobState[8],
        };
        fsm[mobState[2]] = mobFSM;

        // Mob Cast State FSM
        mobFSM = new List<string>()
        {
            mobState[2], mobState[4], mobState[5], mobState[6], mobState[8],
        };
        fsm[mobState[3]] = mobFSM;

        // Mob ATK State FSM
        mobFSM = new List<string>()
        {
            mobState[3], mobState[5], mobState[6], mobState[8],
        };
        fsm[mobState[4]] = mobFSM;

        // Mob Hit State FSM
        mobFSM = new List<string>()
        {
            mobState[3], mobState[4], mobState[6], mobState[8],
        };
        fsm[mobState[5]] = mobFSM;

        // Mob Die State FSM
        mobFSM = new List<string>()
        {
        };
        fsm[mobState[6]] = mobFSM;

        // mob Move State FSM
        fsm[mobState[7]] = mobFSM;

        fsm[mobState[8]] = mobFSM;  


    }

    public List<string> getList(string str)
    {
        return fsm[str];
    }

}
