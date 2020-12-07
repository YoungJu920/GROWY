using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum LoginReturnCode
{
    SUCCESS = 0,
    NO_EXIST_ID = 1,
    NO_MATCH_PW = 2
}

public enum SignUpReturnCode
{
    SUCCESS = 0,
    SAME_ID_EXIST = 1
}

public class LoginManager : Singleton<LoginManager>
{
    [Header("LOGIN")]
    public GameObject LoginPanel;
    public InputField ID_InputField;
    public InputField PW_InputField;
    public Button LoginButton;
    public Button SignUpButton;

    [Header("SIGN UP")]
    public GameObject SignUpPanel;
    public InputField NewID_InputField;
    public InputField NewPW_InputField;
    public InputField NewNickname_InputField;
    public Button OKButton;
    public Button CancleButton;

    [Header("CHOICE CHARACTER")]
    public GameObject SpritePanel;
    public Button[] CharacterBtns;

    [HideInInspector] int SelectedNum = -1;

    public void Init()
    {
        Utility.AddListener(LoginButton, ButtonType.COMMON_BTN, Login);

        Utility.AddListener(SignUpButton, ButtonType.COMMON_BTN, ShowSignUpPanel);

        Utility.AddListener(OKButton, ButtonType.COMMON_BTN, SignUp);

        Utility.AddListener(CancleButton, ButtonType.COMMON_BTN, ShowLoginPanel);

        for (int i = 0; i < 3; i++)
            Utility.AddListener(CharacterBtns[i], ButtonType.SPRITE_BTN, SelectCharacter, i);
    }

    void SelectCharacter(int num)
    {
        SelectedNum = num;
        
        for (int i = 0; i < CharacterBtns.Length; i++)
        {
            Image img = CharacterBtns[i].GetComponent<Image>();

            if (i == num)
            {
                img.color = new Color32(255, 194, 194, 255);
                continue;
            }

            img.color = new Color32(255, 255, 255, 255);
        }
    }

    void Login()
    {
        string id = ID_InputField.text;
        string password = PW_InputField.text;

        ServerManager.Instance.Request("LOGIN", new string[]{id, password}, ResultOfLogin);
    }

    void SignUp()
    {
        string id = NewID_InputField.text;
        string password = NewPW_InputField.text;
        string nickname = NewNickname_InputField.text;
        int class_type = SelectedNum;

        // 공백인지 확인
        if (id == "" || password == "")
            return;

        // 띄어쓰기 제거 후 확인
        if (id.Trim() == "" || password.Trim() == "")
            return;

        // 문자열 길이 제한
        if (id.Length < 3 || id.Length > 10)
        {
            PopupManager.Instance.CreatePopupOneBtn("ID는 3글자 이상, 10글자 이하로 작성해 주세요.");
            return;
        }
        if (password.Length < 4 || password.Length > 12)
        {
            PopupManager.Instance.CreatePopupOneBtn("PW는 4글자 이상, 12글자 이하로 작성해 주세요.");
            return;
        }

        // 캐릭터 선택했는지 확인
        if (class_type == -1)
        {
            PopupManager.Instance.CreatePopupOneBtn("캐릭터를 선택하세요.");
            return;
        }

        ServerManager.Instance.Request("SIGN_UP", new string[]{id, password, nickname, class_type.ToString()}, ResultOfSignUp);
    }

    void ResultOfLogin(string result)
    {
        var list = Utility.ParsingString(result);
        
        if (list == null)
            return;

        LoginReturnCode return_code = (LoginReturnCode)(list["return_code"].AsInt);

        switch(return_code)
        {
            case LoginReturnCode.NO_EXIST_ID:
            {
                PopupManager.Instance.CreatePopupOneBtn("해당하는 아이디가 없습니다.");
                break;
            }

            case LoginReturnCode.NO_MATCH_PW:
            {
                PopupManager.Instance.CreatePopupOneBtn("비밀번호가 다릅니다.");
                break;
            }

            case LoginReturnCode.SUCCESS:
            {
                if (list["user_index"] != null)
                {
                    PlayerPrefs.SetInt("user_index", list["user_index"]);
                    SceneManager.Instance.ChangeScene("TownScene");
                }
                break;
            }
            
        }
    }

    void ResultOfSignUp(string result)
    {
        var list = Utility.ParsingString(result);
        
        if (list == null)
            return;

        SignUpReturnCode return_code = (SignUpReturnCode)(list["return_code"].AsInt);

        switch(return_code)
        {
            case SignUpReturnCode.SAME_ID_EXIST:
            {
                PopupManager.Instance.CreatePopupOneBtn("동일한 아이디가 존재합니다.");
                break;
            }

            case SignUpReturnCode.SUCCESS:
            {
                PopupManager.Instance.CreatePopupOneBtn("아이디 등록에 성공했습니다.", ShowLoginPanel);
                break;
            }
        }
    }

    void ShowLoginPanel()
    {
        LoginPanel.SetActive(true);
        SignUpPanel.SetActive(false);
        SpritePanel.SetActive(false);
    }

    void ShowSignUpPanel()
    {
        LoginPanel.SetActive(false);
        SignUpPanel.SetActive(true);
        SpritePanel.SetActive(true);
    }
}
