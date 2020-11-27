using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;         // 사운드의 파일 이름
    public AudioClip clip;      // 사운드 파일
    private AudioSource source; // 사운드 플레이어

    public float Volumn;
    public bool loop;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.volume = Volumn;
    }

    public void SetVolumn()
    {
        source.volume = Volumn;
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetLoop(bool value)
    {
        source.loop = value;
    }
}


public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] public Sound[] skillSounds;    // 스킬 사운드
    [SerializeField] public Sound[] menuSounds;     // 일반 사운드
    [SerializeField] public AudioClip[] bgms;       // BGM

    public AudioSource audioSource;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < skillSounds.Length; i++)
        {
            GameObject soundObject = new GameObject("Sound File " + i + " " + skillSounds[i].name);
            skillSounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }

        for (int i = 0; i < menuSounds.Length; i++)
        {
            GameObject soundObject = new GameObject("Sound File " + i + " " + menuSounds[i].name);
            menuSounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

    public void PlayMenuSound(string _name)
    {
        for (int i = 0; i < menuSounds.Length; i++)
        {
            if (_name == menuSounds[i].name)
            {
                menuSounds[i].Play();
                return;
            }
        }
    }

    public void PlaySkillSound(string _name)
    {
        for (int i = 0; i < skillSounds.Length; i++)
        {
            if (_name == skillSounds[i].name)
            {
                skillSounds[i].Play();
                return;
            }
        }
    }

    public void PlayBGM(int trackNum)
    {
        //audioSource.volume = 1f;
        audioSource.clip = bgms[trackNum];
        audioSource.Play();
    }

    public void SetBGMVolumn(float _volumn)
    {
        audioSource.volume = _volumn;
    }

    public void PauseBGM(bool value)
    {
        if (value)
            audioSource.Pause();
        else
            audioSource.UnPause();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void FadeOutBGM()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutBGMCoroutine());
    }

    IEnumerator FadeOutBGMCoroutine()
    {
        for (float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            audioSource.volume = i;
            yield return waitTime;
        }

        StopBGM();
    }

    public void FadeInBGM()
    {
        //StopAllCoroutines();
        StartCoroutine(FadeInBGMCoroutine());
    }
    IEnumerator FadeInBGMCoroutine()
    {
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            audioSource.volume = i;
            yield return waitTime;
        }
    }

    public void StopAndPlayBGM(int trackNum)        // 이전 BGM을 FadeOut시키고 새 BGM을 FadeIn
    {
        StartCoroutine(StopAndPlayBGMCoroutine(trackNum));
    }

    IEnumerator StopAndPlayBGMCoroutine(int trackNum)
    {
        yield return StartCoroutine(FadeOutBGMCoroutine());

        PlayBGM(trackNum);

        FadeInBGM();
    }
}
