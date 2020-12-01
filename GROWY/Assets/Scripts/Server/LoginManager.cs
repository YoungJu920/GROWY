using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public enum LoginReturnCode
{
    SUCCESS = 0,
    NO_EXIST_ID = 1,
    NO_MATCH_PW = 2
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
        ServerManager.Instance.Login(ID_InputField.text, PW_InputField.text, ResultOfLogin);
    }

    void SignUp()
    {
        ServerManager.Instance.SignUp(NewID_InputField.text, NewPW_InputField.text, NewNickname_InputField.text, SelectedNum);
    }

    void ResultOfLogin(string result)
    {
        var N = JSON.Parse(result);

        switch((LoginReturnCode)N["return_code"].AsInt)
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
                // if (N["user_index"] != null)    PlayerStatManager.instance.UserIndex = N["user_index"];
                // if (N["nickname"] != null)      PlayerStatManager.instance.PlayerName = N["nickname"];
                // if (N["gold"] != null)          PlayerStatManager.instance.PlayerGold = N["gold"].AsInt;
                // if (N["level"] != null)         PlayerStatManager.instance.PlayerLevel = N["level"].AsInt;
                // if (N["exp"] != null)           PlayerStatManager.instance.PlayerExp = N["exp"].AsInt;
                // if (N["max_hp"] != null)        PlayerStatManager.instance.statDictionary["최대 체력"] = N["max_hp"].AsFloat;
                // if (N["current_hp"] != null)    PlayerStatManager.instance.statDictionary["현재 체력"] = N["current_hp"].AsFloat;
                // if (N["per_hp"] != null)        PlayerStatManager.instance.statDictionary["초당 체력 회복"] = N["per_hp"].AsFloat;
                // if (N["max_mp"] != null)        PlayerStatManager.instance.statDictionary["최대 마나"] = N["max_mp"].AsFloat;
                // if (N["current_mp"] != null)    PlayerStatManager.instance.statDictionary["현재 마나"] = N["current_mp"].AsFloat;
                // if (N["per_mp"] != null)        PlayerStatManager.instance.statDictionary["초당 마나 회복"] = N["per_mp"].AsFloat;
                // if (N["ad"] != null)            PlayerStatManager.instance.statDictionary["공격력"] = N["ad"].AsFloat;
                // if (N["ap"] != null)            PlayerStatManager.instance.statDictionary["주문력"] = N["ap"].AsFloat;
                // if (N["defence"] != null)       PlayerStatManager.instance.statDictionary["방어력"] = N["defence"].AsFloat;
                // if (N["evasion"] != null)       PlayerStatManager.instance.statDictionary["회피"] = N["evasion"].AsFloat;
                // if (N["critical_rate"] != null) PlayerStatManager.instance.statDictionary["크리티컬 확률"] = N["critical_rate"].AsFloat;
                // if (N["move_speed"] != null)    PlayerStatManager.instance.statDictionary["이동속도"] = N["move_speed"].AsFloat;
                // if (N["fire_element"] != null)  PlayerStatManager.instance.statDictionary["화염 속성 강화"] = N["fire_element"].AsFloat;
                // if (N["ice_element"] != null)   PlayerStatManager.instance.statDictionary["냉기 속성 강화"] = N["ice_element"].AsFloat;
                // if (N["light_element"] != null) PlayerStatManager.instance.statDictionary["빛 속성 강화"] = N["light_element"].AsFloat;
                // if (N["dark_element"] != null)  PlayerStatManager.instance.statDictionary["어둠 속성 강화"] = N["dark_element"].AsFloat;
                // if (N["class_type"] != null)
                // {
                //     PlayerStatManager.instance.CharacterThumbnailName = GlobalValue.instance.CharacterThumbnailArray[N["class_type"].AsInt];
                //     PlayerStatManager.instance.CharacterSpriteName = GlobalValue.instance.CharacterSheetNameArray[N["class_type"].AsInt];
                // }

                SceneManager.Instance.ChangeScene("TownScene");
                
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
