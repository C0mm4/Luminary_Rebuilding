using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    static SoundManager s_Instance;
    public static SoundManager Instance
    {
        get
        {
            // if instance is NULL create instance
            if (!s_Instance)
            {
                s_Instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (s_Instance == null)
                    Debug.Log("instance is NULL_SoundManager");
            }
            return s_Instance;
        }
    }



    public static AudioSource inPlayBGM;
    public AudioSource attackSound;
    public AudioSource nomalSkillSoundEffect;
    public AudioSource playerHitSound;
    public AudioSource[] mobHitSound;




    public bool loadSkillSound(string skillClass)
    {
        try
        {
            attackSound = gameObject.AddComponent<AudioSource>();
            attackSound.clip = Resources.Load<AudioClip>("Audio/Skill/Flame");
        }
        catch (UnityException e)
        {
            Debug.LogError("Failed to load inPlayBGM: " + "Audio/Skill/" + skillClass);
            return false;
        }
        return true;
        
    }


    //Audio play_BGM
    public void playBGM()
    {
        inPlayBGM.Play();
    }

    //Audio play_Skill
    public void playSkillSound(int skillNum)
    {
        nomalSkillSoundEffect.Play();
    }

    public void playAttackSound()
    {
        Debug.Log("attackSound.Play");
        attackSound.Play();
    }

}
