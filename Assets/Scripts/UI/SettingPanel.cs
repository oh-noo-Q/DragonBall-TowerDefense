using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public Button privacyButton, termsButton, closeButton, closeRateButton, rateUsButton;
    public GameObject rateSuccess, settingPanel;
    public Toggle vibrationToggle, soundToggle;
    public List<Image> starGroup;
    public Image vibraToggleBg, soundToggleBg;
    public RectTransform vibraHandle, soundHandle;
    public Sprite toggleBgOn, toggleBgOff, starYell, starWhite;
    public float leftPos, rightPos;

    private int _currentRateIndex = -1;


    private void Start()
    {
        privacyButton.onClick.AddListener(PrivacyOnClick);
        termsButton.onClick.AddListener(TermsOnClick);
        closeButton.onClick.AddListener(CloseOnClick);
        //closeRateButton.onClick.AddListener(CloseRateOnClick);
        vibrationToggle.onValueChanged.AddListener(VibraClick);
        soundToggle.onValueChanged.AddListener(SoundClick);
        //rateUsButton.onClick.AddListener(OnRateUsClick);
    }


    private void OnEnable()
    {
        Time.timeScale = 0f;
        UpdateUI();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        //ShowRateSuccess(false);
    }

    private void UpdateUI()
    {
        soundToggle.isOn = UserData.SoundSetting;
        vibrationToggle.isOn = UserData.VibrationSetting;

        UpdateUISound(UserData.SoundSetting);
        UpdateUIVibra(UserData.VibrationSetting);
        SetStar(4);
    }

    private void UpdateUIVibra(bool isOn)
    {
        vibraToggleBg.sprite = isOn ? toggleBgOn : toggleBgOff;
        vibraHandle.localPosition = new Vector2(isOn ? rightPos : leftPos, 0);
    }

    public void SetStar(int index)
    {
        _currentRateIndex = index;
        for (int i = 0; i < starGroup.Count; i++)
        {
            bool active = i <= index;
            starGroup[i].sprite = active ? starYell : starWhite;
        }
    }

    private void UpdateUISound(bool isOn)
    {
        soundToggleBg.sprite = isOn ? toggleBgOn : toggleBgOff;
        soundHandle.localPosition = new Vector2(isOn ? rightPos : leftPos, 0);
    }

    private void SoundClick(bool toggle)
    {
        UserData.SoundSetting = toggle;
        UpdateUISound(toggle);
        EventDispatcher.Instance.PostEvent(EventID.Mute, toggle);
    }

    private void VibraClick(bool toggle)
    {
        UserData.VibrationSetting = toggle;
        UpdateUIVibra(toggle);
    }

    private void CloseOnClick()
    {
        //AdsController.Instances.ShowInterstitial((() => { gameObject.SetActive(false); }),
        //    InterstitialPositionType.Close);
        gameObject.SetActive(false);
    }

    private void CloseRateOnClick()
    {
        //AdsController.Instances.ShowInterstitial((() => { ShowRateSuccess(false); }),
        //    InterstitialPositionType.Close);
    }

    private void OnRateUsClick()
    {
//        if (_currentRateIndex != -1)
//        {
//            UserData.HasRate = true;
//            GameAnalytics.LogRateUsShow(_currentRateIndex + 1);
//            if (_currentRateIndex == 4)
//            {
//                Debug.Log(Application.productName);
//#if UNITY_ANDROID
//                Application.OpenURL("market://details?id=" + Application.identifier);
//                GameAnalytics.LogRateUs5Stars();
//// #elif UNITY_IOS
////                 Application.OpenURL("url");
////                 GameAnalytics.LogRateUs5Stars();
//// #else
//                // Application.OpenURL("itms-apps://itunes.apple.com/app/1509205958");
//#endif
//            }

//            ShowRateSuccess(true);
//        }
//        else
//        {
//            ShowRateSuccess(true);
//        }
    }

    private void ShowRateSuccess(bool showRate)
    {
        rateSuccess.SetActive(showRate);
        settingPanel.SetActive(!showRate);
    }

    private void TermsOnClick()
    {
        Application.OpenURL("https://sites.google.com/view/imosystermsofuse");
    }

    private void PrivacyOnClick()
    {
        Application.OpenURL("http://sites.google.com/site/imoprivacypolicy/");
    }
}