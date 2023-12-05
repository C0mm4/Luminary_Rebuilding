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
    public AudioSource mobHitSound;


    public bool soundSetupInit(float gameVolume, float effectVolume, float musicVolume, bool isGameInit)
    {
        try
        {
            attackSound = gameObject.AddComponent<AudioSource>();
            mobHitSound = gameObject.AddComponent<AudioSource>();
            if (isGameInit)
            {
                inPlayBGM = gameObject.AddComponent<AudioSource>();
                loadBgm("GameTitle");
            }
            nomalSkillSoundEffect = gameObject.AddComponent<AudioSource>();

            attackSound.volume = effectVolume * gameVolume;
            mobHitSound.volume = effectVolume * gameVolume;
            inPlayBGM.volume = musicVolume * gameVolume;

            loadSkillSound("test");
            loadHitSound("test");

            return true;
        }
        catch (UnityException e)
        {
            Debug.LogError("Failed to setup sound: " + "volume ctrl");
            return false;
        }
    }

    public bool loadSkillSound(string skillClass)
    {
        try
        {
            attackSound.clip = Resources.Load<AudioClip>("Audio/Skill/Flame");
        }
        catch (UnityException e)
        {
            Debug.LogError("Failed to load inPlayBGM: " + "Audio/Skill/Flame");
            return false;
        }
        return true;
    }

    public bool loadHitSound(string skillClass)
    {
        try
        {
            
            mobHitSound.clip = Resources.Load<AudioClip>("Audio/Skill/NomalHit");
        }
        catch (UnityException e)
        {
            Debug.LogError("Failed to load inPlayBGM: " + "Audio/Skill/NomalHit");
            return false;
        }
        return true;
    }

    public bool loadBgm(string bgmName)
    {
        try
        {
            inPlayBGM.clip = Resources.Load<AudioClip>("Audio/BGM/" + bgmName);
            Debug.Log("BGM Load : " + bgmName);
        }
        catch (UnityException e)
        {
            Debug.LogError("Failed to load inPlayBGM: " + "Audio/BGM/" + bgmName);
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

    public void playMopHitSound()
    {
        mobHitSound.Play();
    }

    public void playAttackSound()
    {
        Debug.Log("attackSound.Play");
        attackSound.Play();
    }

    public void bgmChange(string targetBGM, float fadeTime)
    {
        Debug.Log("BGM FadeOut");
        StartCoroutine(FadeOut(fadeTime, targetBGM));
    }

    public IEnumerator FadeOut(float fadeTime, string targetBGM)
    {
        Debug.Log("BGM FadeOut");
        float startVolume = inPlayBGM.volume;

        while (inPlayBGM.volume > 0)
        {
            inPlayBGM.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        inPlayBGM.Stop();
        inPlayBGM.volume = startVolume;
        //yield return new WaitForSeconds(0.1f);

        loadBgm(targetBGM);
        StartCoroutine(FadeIn(fadeTime));
    }

    public IEnumerator FadeIn(float fadeTime)
    {

        float startVolume = inPlayBGM.volume;
        inPlayBGM.volume = 0; // 볼륨을 0으로 설정
        inPlayBGM.Play(); // 사운드 재생 시작

        while (inPlayBGM.volume < startVolume)
        {
            inPlayBGM.volume += Time.deltaTime / fadeTime * startVolume; // 볼륨을 점차적으로 증가

            yield return null;
        }

        inPlayBGM.volume = startVolume; // 볼륨을 최대로 설정
    }

}
