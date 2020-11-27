using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopupManager : Singleton<PopupManager>
{
    [Header("Transform")]
    public Transform  canvasTR;

    [Header("PopupPrefabs")]
    public GameObject PopupOneBtn;
    public GameObject PopupTwoBtn;

    public void CreatePopupOneBtn(string text, Action method = null, float scale = 1.0f)
    {
        AudioManager.Instance.PlayMenuSound("popup0");

        // 팝업창 생성
        GameObject window = Instantiate(PopupOneBtn);
        window.transform.SetParent(GameObject.Find("baseCanvas").transform, false);
        window.transform.localScale = new Vector3(scale, scale, 1.0f);

        // 텍스트 길이에 따라 팝업창 사이즈 조정
        RectTransform rt = window.GetComponent<RectTransform>();
        int multiple;
        if (text.Length < 12)
            multiple = 35;
        else if (text.Length >= 12 && text.Length < 20)
            multiple = 30;
        else
            multiple = 25;
        rt.sizeDelta = new Vector2(text.Length * multiple, 250);

        // 텍스트 초기화
        Text mainText = window.transform.Find("MainText").GetComponent<Text>();
        mainText.text = text;

        // 버튼 OnClick
        Button OkBtn = window.transform.Find("OK_Btn").GetComponent<Button>();
        OkBtn.onClick.AddListener(() => {
            AudioManager.Instance.PlayMenuSound("click0");
            if (method != null)
                method();
            Destroy(window);
        });
    }

    public void CreatePopupTwoBtn(string text, Action method = null, float scale = 1.0f)
    {
        AudioManager.Instance.PlayMenuSound("popup0");

        // 팝업창 생성
        GameObject window = Instantiate(PopupTwoBtn);
        window.transform.SetParent(GameObject.Find("popupCanvas").transform, false);
        window.transform.localScale = new Vector3(scale, scale, 1.0f);


        // 텍스트 길이에 따라 팝업창 사이즈 조정
        RectTransform rt = window.GetComponent<RectTransform>();
        int multiple;
        if (text.Length < 12)
            multiple = 35;
        else if (text.Length >= 12 && text.Length < 20)
            multiple = 30;
        else
            multiple = 25;
        rt.sizeDelta = new Vector2(text.Length * multiple, 250);

        // 텍스트 초기화
        Text mainText = window.transform.Find("MainText").GetComponent<Text>();
        mainText.text = text;

        // 버튼 OnClick
        Button OkBtn = window.transform.Find("OK_Btn").GetComponent<Button>();
        OkBtn.onClick.AddListener(() => {
            AudioManager.Instance.PlayMenuSound("click0");
            if (method != null)
                method();
            Destroy(window);
        });

        Button CancleBtn = window.transform.Find("Cancle_Btn").GetComponent<Button>();
        CancleBtn.onClick.AddListener(() => {
            AudioManager.Instance.PlayMenuSound("click0");
            Destroy(window);
        });
    }
}