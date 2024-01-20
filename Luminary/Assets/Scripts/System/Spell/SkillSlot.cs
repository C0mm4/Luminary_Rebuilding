using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot
{
    // Get Spell in Command pattern
    private Spell skillCommand = null;


    public SkillSlot()
    {
    }

    // Set Spell command target
    public void setCommand(Spell command)
    {
        skillCommand = command;
    }
    // Set Command to Null
    public void deSetCommand()
    {
        skillCommand = null;
    }

    public Spell getSpell()
    {
        return skillCommand;
    }

    // Use Spell in triggered
    public void useSkill()
    {

        if (GameManager.player.GetComponent<Player>().status.currentMana >= skillCommand.data.circle)
        {

            Vector3 pos = GameManager.inputManager.mouseWorldPos;
            GameManager.player.GetComponent<Charactor>().changeState(new PlayerCastingState(getSpell(), GameManager.inputManager.mouseWorldPos));


        }
        else
        {
            GameManager.Instance.uiManager.textUI("Not Enough Mana");
        }
        

    }


    // Returning skillCommand
    public bool isSet()
    {
        if (skillCommand != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // It Doesn't work well
    public GameObject GetClosestObjectToMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

        float closestDistance = float.MaxValue;
        GameObject closestObject = null;

        if(colliders.Length == 0)
        {
            Debug.Log("Doesn't Colliding");
        }

        foreach (Collider2D collider in colliders)
        {
            if(collider.gameObject.tag == "Mob")
            {
                float distance = Vector2.Distance(collider.transform.position, mousePosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = collider.gameObject;
                }
            }

        }

        return closestObject;
    }
}