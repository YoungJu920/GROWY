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

    public static IEnumerator WebRequest(POST[] posts, string php, Action<string> callback, bool printLog = false)
    {
        WWWForm form = new WWWForm();

        for (int i = 0; i < posts.Length; i++)
        {
            if (posts[i].encoding != null)
                form.AddField(posts[i].key, posts[i].value, posts[i].encoding);
            else
                form.AddField(posts[i].key, posts[i].value);
        }

        UnityWebRequest request = new UnityWebRequest();

        string result = "";

        using (request = UnityWebRequest.Post(php, form))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                System.Text.Encoding enc = System.Text.Encoding.GetEncoding("euc-kr");
                result = enc.GetString(request.downloadHandler.data);
                
                if (printLog)
                    Debug.Log(result);
            }
        }
        
        callback(result);
    }
}
