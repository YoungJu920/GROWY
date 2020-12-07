using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    [SerializeField] protected SceneType sceneType = SceneType.NONE;
    public SceneType Type { get { return sceneType;} }

    void Start()
    {
        StartScene();
        SetCurrScene();
        PlayBGM();
    }

    void PlayBGM()
    {
        AudioManager.Instance.PlayBGMSound(sceneType.ToString());
        AudioManager.Instance.FadeInBGM();
    }

    void SetCurrScene()
    {
        SceneManager.Instance.CurrScene = this;
        Debug.Log("SetCurrScene : " + this.name);
    }

    protected virtual void StartScene()
    {
    }

    protected virtual void EndScene()
    {
    }
}
