using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func
{
    public static PointPosition GetPointPosition(Vector2 baseP, Vector2 targetP)
    {
        float horizonDist = targetP.x - baseP.x;
        float verticalDist = targetP.y - baseP.y;

        if (Mathf.Abs(horizonDist) >= Mathf.Abs(verticalDist))
        {
            if (horizonDist > 0)
            {
                return PointPosition.Right;
            }
            else
            {
                return PointPosition.Left;
            }

        }
        else
        {
            if (verticalDist > 0)
            {
                return PointPosition.Up;
            }
            else
            {
                return PointPosition.Down;
            }
        }
    }
    public static void SetRectTransform(GameObject go, Vector3 pos = default)
    {
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.transform.SetParent(GameManager.Instance.canvas.transform, false);
        rt.transform.localScale = Vector3.one;
        rt.transform.localPosition = pos;
    }

    public static SerializedPlayerStatus calcStatus(SerializedPlayerStatus status)
    {

        // HP status Calculate
        status.maxHP = (int)((status.baseHP + status.strength / 2) * (status.pIncreaseMaxHP + 1));
        Debug.Log(status.maxHP);
        // damage Calculate
        status.finalDMG = (int)Math.Round((status.baseDMG * ((1 + (0.1 * status.Intellect))) + (0.02 * status.strength) + (0.03 * status.dexterity) + (1 + status.increaseDMG) / 100));

        // speed Calculate
        status.speed = (int)Math.Round((status.basespeed + status.increaseSpeed) * ((status.dexterity * 0.05) + 0.95) * (status.pIncreaseSpeed + 1));
        return status;
    }

    public static int[] xpos = new int[4] { 0, 0, 1, -1 };
    public static int[] ypos = new int[4] { 1, -1, 0, 0 };

    public static int[] xXpos = new int[4] { -1, -1, 1, 1 };
    public static int[] yXpos = new int[4] { -1, 1, 1, -1 };
}