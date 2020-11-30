using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.SceneManagement;
#pragma warning disable 0618

public class LoginManager : MonoBehaviour
{
    [Header("LOGIN")]
    public GameObject LoginPanel;
    public InputField ID_InputField;
    public InputField PW_InputField;
    public Button LoginButton = null;
    public Button SignUpButton = null;
    string LoginURL;

    [Header("SIGN UP")]
    public GameObject SignUpPanel;
    public GameObject SpritePanel;
    public InputField NewID_InputField;
    public InputField NewPW_InputField;
    public InputField NewNickname_InputField;
    public Button OKButton = null;
    public Button CancleButton = null;
    string SignUpURL;
    
    string LoadExpTableURL;

    PopupManager thePopup;

    public Button[] CharacterBtns;
    public int SelectedNum = -1;

    void Start()
    {
        Utility.AddListener(LoginButton, ButtonType.COMMON_BTN, Login);

        Utility.AddListener(SignUpButton, ButtonType.COMMON_BTN, ShowSignUpPanel);

        Utility.AddListener(OKButton, ButtonType.COMMON_BTN, SignUp);

        Utility.AddListener(CancleButton, ButtonType.COMMON_BTN, ShowLoginPanel);

        for (int i = 0; i < 3; i++)
            Utility.AddListener(CharacterBtns[i], ButtonType.SPRITE_BTN, SelectCharacter, i);
        
        
        LoginURL = "http://youngju.tk/Roguelike/Login/Login.php";
        SignUpURL = "http://youngju.tk/Roguelike/Login/SignUp.php";
        LoadExpTableURL = "http://youngju.tk/Roguelike/Exp/LoadExpTable.php";

        thePopup = FindObjectOfType<PopupManager>();

        // BGM 재생
        AudioManager.Instance.PlayBGMSound(SceneType.TITLE_SCENE.ToString());
        AudioManager.Instance.FadeInBGM();
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

    IEnumerator LoadEXPTableCoroutine()
    {
        WWWForm form = new WWWForm();

        WWW webRequest = new WWW(LoadExpTableURL, form);
        yield return webRequest;

        System.Text.Encoding enc = System.Text.Encoding.GetEncoding("euc-kr");
        string sz = enc.GetString(webRequest.bytes);

        if (sz.Contains("Load Exp Table Success!!"))
        {
            var N = JSON.Parse(sz);
            List<int> exp_table = new List<int>();

            for (int i = 0; i < N.Count; i++)
            {
                exp_table.Add(N[i].AsInt);
            }

            //PlayerStatManager.instance.ExpTable = exp_table;
        }
    }

    void Login()
    {
        StartCoroutine(LoginCoroutine());
    }

    void SignUp()
    {
        StartCoroutine(SignUpCoroutine());
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

    IEnumerator LoginCoroutine()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_id", ID_InputField.text);
        form.AddField("Input_pass", PW_InputField.text);

        WWW webRequest = new WWW(LoginURL, form);
        yield return webRequest;

        System.Text.Encoding enc = System.Text.Encoding.GetEncoding("euc-kr");
        string sz = enc.GetString(webRequest.bytes);
        Debug.Log(sz);

        if (sz.Contains("ID does not exist"))
        {
            thePopup.CreatePopupOneBtn("해당하는 아이디가 없습니다.", null);
            yield return null;
        }
            
        else if (sz.Contains("Pass does not Match"))
        {
            thePopup.CreatePopupOneBtn("비밀번호가 다릅니다.", null);
            yield return null;
        }
            
        else
        {
            var N = JSON.Parse(sz);

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

            // 경험치 테이블 불러오기
            yield return StartCoroutine(LoadEXPTableCoroutine());

            // BGM 꺼주기
            AudioManager.Instance.FadeOutBGM();     // StopBGM 포함

            SceneManager.LoadScene("TownScene");
        }
    }

    IEnumerator SignUpCoroutine()
    {
        // 공백인지 확인
        if (NewID_InputField.text == "" || NewPW_InputField.text == "")
            yield break;

        // 띄어쓰기 제거 후 확인
        string id = NewID_InputField.text;
        string pw = NewPW_InputField.text;
        if (id.Trim() == "" || pw.Trim() == "")
            yield break;

        // 문자열 길이 제한
        if (id.Length < 3 || pw.Length > 10)
        {
            thePopup.CreatePopupOneBtn("ID는 3글자 이상, 10글자 이하로 작성해 주세요.");
            yield break;
        }
        if (pw.Length < 4 || pw.Length > 12)
        {
            thePopup.CreatePopupOneBtn("PW는 4글자 이상, 12글자 이하로 작성해 주세요.");
            yield break;
        }

        // 캐릭터 선택했는지 확인
        if (SelectedNum == -1)
        {
            thePopup.CreatePopupOneBtn("캐릭터를 선택하세요.", null);
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("Input_id", NewID_InputField.text);
        form.AddField("Input_pass", NewPW_InputField.text);
        form.AddField("Input_nickname", NewNickname_InputField.text, System.Text.Encoding.GetEncoding("euc-kr"));
        form.AddField("Input_class_type", SelectedNum);

        WWW webRequest = new WWW(SignUpURL, form);
        yield return webRequest;

        System.Text.Encoding enc = System.Text.Encoding.GetEncoding("euc-kr");
        string sz = enc.GetString(webRequest.bytes);
        Debug.Log(sz);

        if (sz.Contains("ID does exist."))
            thePopup.CreatePopupOneBtn("동일한 아이디가 존재합니다.", null);

        //if (sz.Contains("Create sucess."))
            //thePopup.CreatePopupOneBtn("아이디 등록에 성공했습니다.", new GlobalValue.MethodPointer(ReturnLoginWindow));
    }

    // void ReturnLoginWindow(NPCManager npc = null)
    // {
    //     LoginPanel.SetActive(true);
    //     spritePanel.SetActive(false);
    //     SignUpPanel.SetActive(false);
    // }
}
