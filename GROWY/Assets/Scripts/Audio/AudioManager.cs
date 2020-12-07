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
    [Header("SOUND DICTIONARY")]
    [SerializeField] StringSoundDictionary menuSoundDictionary;
    [SerializeField] StringSoundDictionary skillSoundDictionary;
    [SerializeField] StringSoundDictionary bgmSoundDictionary;

    [SerializeField] AudioSource audioSource;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    public void Init()
    {
        InitSoundObjects(menuSoundDictionary);
        InitSoundObjects(skillSoundDictionary);
    }

    void InitSoundObjects(StringSoundDictionary soundDictionary)
    {
        foreach (KeyValuePair<string, Sound> item in soundDictionary)
        {
            Sound sound = item.Value;
            GameObject soundObject = new GameObject("Sound File : " + sound.name);
            sound.SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

    public void PlayMenuSound(string key)
    {
        if (menuSoundDictionary.ContainsKey(key))
            menuSoundDictionary[key].Play();
    }

    public void PlaySkillSound(string key)
    {
        if (skillSoundDictionary.ContainsKey(key))
            skillSoundDictionary[key].Play();
    }

    public void PlayBGMSound(string key)
    {
        if (bgmSoundDictionary.ContainsKey(key))
        {
            audioSource.clip = bgmSoundDictionary[key].clip;
            audioSource.Play();
        }
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

    public void StopAndPlayBGM(string key)        // 이전 BGM을 FadeOut시키고 새 BGM을 FadeIn
    {
        StartCoroutine(StopAndPlayBGMCoroutine(key));
    }

    IEnumerator StopAndPlayBGMCoroutine(string key)
    {
        yield return StartCoroutine(FadeOutBGMCoroutine());

        PlayBGMSound(key);

        FadeInBGM();
    }
}
