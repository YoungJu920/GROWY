using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : Scene
{
    protected override void StartScene()
    {
        LoginManager.Instance.Init();
        AudioManager.Instance.Init();
        PopupManager.Instance.Init();
        ServerManager.Instance.Init();
    }

    protected override void EndScene()
    {
        LoginManager.Instance.OnDestroy();
        AudioManager.Instance.OnDestroy();
        PopupManager.Instance.OnDestroy();
        ServerManager.Instance.OnDestroy();
    }
}