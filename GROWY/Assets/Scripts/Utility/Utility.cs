using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
}
