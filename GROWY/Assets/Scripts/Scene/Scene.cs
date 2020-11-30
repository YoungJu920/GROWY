using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    [SerializeField] protected SceneType sceneType = SceneType.NONE;
    public SceneType Type { get { return sceneType;} }

    void Start()
    {
        PlayBGM();
        SetCurrScene();
        Init();
    }

    void PlayBGM()
    {
        AudioManager.Instance.PlayBGMSound(sceneType.ToString());
        AudioManager.Instance.FadeInBGM();
    }

    void SetCurrScene()
    {
        SceneManager.Instance.CurrScene = this;
    }

    protected virtual void Init()
    {
    }
}
