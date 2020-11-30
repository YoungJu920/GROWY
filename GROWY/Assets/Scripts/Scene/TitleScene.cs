using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : Scene
{
    protected override void Init()
    {
        LoginManager.Instance.Init();
        AudioManager.Instance.Init();
    }
}
