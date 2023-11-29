using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : AIModel
{
    private bool[] patturns = new bool[3];
    private bool isMove = false;
    private float moveTime;

    public override void Update()
    {
        if(GameManager.player != null)
        {
            // if is move, wait for seconds, to next action
            if (isMove)
            {
                if (Time.time - moveTime >= 1f)
                {
                    isMove = false;
                    target.setIdleState();
                    Debug.Log("Move End");
                    target.changeState(new MobCastState(0f, 0));
                }
            }
            else
            {
                string state = target.getState().GetType().Name;
                if (state != "MobStunState" && state != "MobCastState" && state != "MobATKState")
                {

                    // ü�� ���� ��ġ ���ϸ� ������� ����
                    if (target.HPPercent() <= 0.25f && !patturns[0])
                    {
                        patturns[0] = true;
                        target.changeState(new MobCastState(2f, 3));
                    }
                    else if (target.playerDistance().magnitude <= target.data.attackRange[2])
                    {
                        if (Time.time - target.lastAttackT[2] >= target.data.castCool[2])
                        {
                            // Eating Patturn
                        }
                        else
                        {
                            if (target.playerDistance().magnitude <= target.data.attackRange[1])
                            {
                                if (Time.time - target.lastAttackT[1] >= target.data.castCool[1])
                                {
                                    // Slime Rain Patturn
                                    target.changeState(new MobCastState(2f, 1));
                                }
                                else
                                {
                                    if (Time.time - moveTime >= 2f)
                                    {
                                        moveTime = Time.time;
                                        isMove = true;
                                        target.changeState(new MobChaseState());
                                    }
                                }
                            }
                            else
                            {
                                if (Time.time - moveTime >= 2f)
                                {
                                    moveTime = Time.time;
                                    isMove = true;
                                    target.changeState(new MobChaseState());
                                }

                            }
                        }
                    }
                    else if (target.playerDistance().magnitude <= target.data.attackRange[1])
                    {
                        if (Time.time - target.lastAttackT[1] >= target.data.castCool[1])
                        {
                            // Slime Rain Patturn
                            target.changeState(new MobCastState(2f, 1));
                        }
                        else
                        {
                            if (Time.time - moveTime >= 2f)
                            {
                                moveTime = Time.time;
                                isMove = true;
                                target.changeState(new MobChaseState());
                            }
                        }
                    }
                    // If move cooltime is done, move
                    else if (Time.time - moveTime >= 2f)
                    {
                        moveTime = Time.time;
                        isMove = true;
                        target.changeState(new MobChaseState());
                    }
                }
            }
        }
    }
}
