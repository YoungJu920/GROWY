using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

public struct POST
{
    public string key;
    public string value;
    public System.Text.Encoding encoding;

    public POST(string key, string value, string encoding_type = null)
    {
        this.key = key;
        this.value = value;
        
        if (encoding_type != null)
            this.encoding = System.Text.Encoding.GetEncoding(encoding_type);
        else
            this.encoding = null;
    }

    public POST(string key, int value)
    {
        this.key = key;
        this.value = value.ToString();
        this.encoding = null;
    }
}

public class ServerManager : Singleton<ServerManager>
{
    public void Login(string id, string password, Action<string> action)
    {
        StartCoroutine(LoginCoroutine(id, password, action));
    }

    public void SignUp(string id, string password, string nickname, int class_type)
    {

    }

    public void Test(string s)
    {

    }

    IEnumerator LoginCoroutine(string id, string password, Action<string> action)
    {
        // Action action = Test1;
        // action += Test2;

        // var first = action.GetInvocationList()[0];
        // first.DynamicInvoke();

        string result = "";

        yield return StartCoroutine(Utility.WebRequest(
            new POST[] { new POST("Input_id", id), new POST("Input_pass", password) }
            , DefsPHP.Login_PHP
            , (x) => { result = x; }
            , true
        ));

        if (result == "")
            yield return null;

        action?.Invoke(result);
    }

    IEnumerator SignUpCoroutine(string id, string password, string nickname, int class_type)
    {
        // 공백인지 확인
        if (id == "" || password == "")
            yield break;

        // 띄어쓰기 제거 후 확인
        if (id.Trim() == "" || password.Trim() == "")
            yield break;

        // 문자열 길이 제한
        if (id.Length < 3 || id.Length > 10)
        {
            PopupManager.Instance.CreatePopupOneBtn("ID는 3글자 이상, 10글자 이하로 작성해 주세요.");
            yield break;
        }
        if (password.Length < 4 || password.Length > 12)
        {
            PopupManager.Instance.CreatePopupOneBtn("PW는 4글자 이상, 12글자 이하로 작성해 주세요.");
            yield break;
        }

        // 캐릭터 선택했는지 확인
        if (class_type == -1)
        {
            PopupManager.Instance.CreatePopupOneBtn("캐릭터를 선택하세요.");
            yield break;
        }

        string result = "";

        yield return StartCoroutine(Utility.WebRequest(
            new POST[] { new POST("Input_id", id), new POST("Input_pass", password), new POST("Input_nickname", nickname, "euc-kr"), new POST("Input_class_type", class_type) }
            , DefsPHP.SignUp_PHP
            , (x) => { result = x; }
            , true
        ));

        if (result.Contains("ID does exist."))
            PopupManager.Instance.CreatePopupOneBtn("동일한 아이디가 존재합니다.");

        // if (result.Contains("Create sucess."))
        //     PopupManager.Instance.CreatePopupOneBtn("아이디 등록에 성공했습니다.", ShowLoginPanel);
    }
}
