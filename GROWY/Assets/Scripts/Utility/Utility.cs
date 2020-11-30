using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
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

    public static IEnumerator WebRequest(Dictionary<string, string> fields, string php, Action<string> callback, bool printLog = false)
    {
        WWWForm form = new WWWForm();

        foreach(KeyValuePair<string, string> item in fields)
        {
            form.AddField(item.Key, item.Value);
        }

        WWW webRequest = new WWW(php, form);
        yield return webRequest;

        System.Text.Encoding enc = System.Text.Encoding.GetEncoding("euc-kr");
        string sz = enc.GetString(webRequest.bytes);
        
        if (printLog)
            Debug.Log(sz);

        callback(sz);
    }
}
