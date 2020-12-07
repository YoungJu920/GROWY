using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using System;

public class Utility
{
    public static void AddListener(Button button, ButtonType type, Action action = null)
    {
        if (button != null)
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlayMenuSound(type.ToString());
                action?.Invoke();
            });
    }

    public static void AddListener(Button button, ButtonType type, Action<int> action = null, int num = 0)
    {
        if (button != null)
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlayMenuSound(type.ToString());
                action?.Invoke(num);
            });
    }

    public static JSONNode ParsingString(string result)
    {
        JSONNode N = JSON.Parse(result);

        if (N["return_code"] == null)
        {
            Debug.Log("return code was not inserted.");
            return null;
        }

        return N;
    }
}
