using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using System;

public struct POST
{
    public string php;
    public POST_ITEM[] post_items;

    public POST(string php, POST_ITEM[] post_items)
    {
        this.php = php;
        this.post_items = post_items;
    }
}

public struct POST_ITEM
{
    public string key { get; set; }
    public System.Text.Encoding encoding { get; set; }

    public POST_ITEM(string key, System.Text.Encoding encoding = null)
    {
        this.key = key;
        this.encoding = encoding;
    }
}

public class ServerManager : Singleton<ServerManager>
{
    private Dictionary<string, POST> post_forms;

    public void Init()
    {
        post_forms = new Dictionary<string, POST>();

        post_forms.Add( "LOGIN"
            , new POST(DefsPHP.Login_PHP
            , new POST_ITEM[]{ new POST_ITEM("Input_id"), new POST_ITEM("Input_pass") }) );

        post_forms.Add( "SIGN_UP"
            , new POST(DefsPHP.SignUp_PHP
            , new POST_ITEM[]{ new POST_ITEM("Input_id"), new POST_ITEM("Input_pass"),
            new POST_ITEM("Input_nickname", GetEncoding("euc-kr")), new POST_ITEM("Input_class_type") }) );
    }

    public void Request(string post_key, string[] values, Action<string> callback)
    {
        StartCoroutine(RequestCoroutine(post_key, values, callback));
    }

    IEnumerator RequestCoroutine(string post_key, string[] values, Action<string> callback)
    {
        string php = post_forms[post_key].php;
        POST_ITEM[] post_items = post_forms[post_key].post_items;

        if (values.Length != post_items.Length)
            yield break;

        string result = "";

        WWWForm form = new WWWForm();

        for (int i = 0; i < post_items.Length; i++)
        {
            if (post_items[i].encoding == null)
                form.AddField(post_items[i].key, values[i]);
            else
                form.AddField(post_items[i].key, values[i], post_items[i].encoding);
        }

        UnityWebRequest request = new UnityWebRequest();

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
                Debug.Log(result);
            }
        }

        callback?.Invoke(result);
    }

    System.Text.Encoding GetEncoding(string key)
    {
        return System.Text.Encoding.GetEncoding(key);
    }
}
