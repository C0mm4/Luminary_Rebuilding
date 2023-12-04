using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public Stack<State> stateStack = new Stack<State>();
    private State currentState = null;

    private Charactor target;

    public StateMachine(Charactor chr)
    {
        target = chr;
    }

    // change charator state
    public void changeState(State newState)
    {
        if(newState == null)
        {
            currentState = null;
        }
        // check this first regist state
        else if (currentState != null) {
            // if stun state setting idle state
            if(newState.GetType().Name == "PlayerStunState" || newState.GetType().Name == "MobStunState")
            {
                Debug.Log("Stun Detect");
                setIdle();
            }
            // check FSM
            if (GameManager.FSM.getList(currentState.GetType().Name).Contains(newState.GetType().Name))
            {
                if(newState.GetType().Name == "PlayerStunState" || newState.GetType().Name == "MobStunState")
                {
                    stateStack.Push(currentState);
                    currentState = newState;

                    currentState.EnterState(target);
                }
                // past state add to stack, new state regist on currentstate
                else if (currentState.GetType().Name != newState.GetType().Name)
                {
                    // Save Previous State
                    stateStack.Push(currentState);

                    // change New State
                    currentState = newState;

                    // State Enter Logic Process
                    currentState.EnterState(target);
                }
                // if currentState is Equal State
                else
                {
                    // change New State
                    currentState = newState;

                    // State Enter Logic Process
                    currentState.EnterState(target);
                }
            }
        }
        else
        {
            currentState = newState;

            currentState.EnterState(target);
        }
    }

    // state update on frame
    public void updateState()
    {
        if(currentState != null )
        {
            currentState.UpdateState();
        }
    }

    // exit currentstate
    public void exitState()
    {
        currentState.ExitState();
        if (stateStack.Count > 0)
        {
            currentState = stateStack.Pop();

            currentState.ReSetState(target);
        }
    }
    // return current state name to string
    public string getStateStr()
    {
        return currentState.GetType().Name;
    }
    // return current state
    public State getState()
    {
        return currentState;
    }

    // set current state on idle and state stack empty
    public void setIdle()
    {
        while(stateStack.Count > 0)
        {
            // empty state stack
            exitState();
        }
        // if last state is not idle, set idle state
        if(getStateStr() != "MobIdleState" || getStateStr() != "PlayerIdleState")
        {
            if(target.tag == "Player")
            {
                currentState = new PlayerIdleState();
                currentState.EnterState(target);
            }
            else
            {
                currentState = new MobIdleState();
                currentState.EnterState(target);
            }
        }
    }
}
