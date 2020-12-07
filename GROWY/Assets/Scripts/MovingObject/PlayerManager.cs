using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] PlayerController thePlayer;

    public void Init()
    {
        thePlayer.Init();
    }
}
