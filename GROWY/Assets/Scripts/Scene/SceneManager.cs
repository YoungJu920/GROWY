using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] Scene currScene;
    public Scene CurrScene { get { return currScene; } set { currScene = value; } }

    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

        AudioManager.Instance.FadeOutBGM();
    }
}
