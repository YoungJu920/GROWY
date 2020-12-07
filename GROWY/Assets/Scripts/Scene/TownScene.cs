using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScene : Scene
{
    protected override void StartScene()
    {
        PlayerManager.Instance.Init();
        PlayerDataManager.Instance.Init();
    }

    protected override void EndScene()
    {
        
    }
}
