using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionSetting
{
    Music,
    SFX,
    Vibration,

}
public class SettingPanel : MonoBehaviour
{
    public Button privacyButton, termsButton, closeButton, closeRateButton, rateUsButton;
    public GameObject rateSuccess, settingPanel;
    public Button vibrationBtn, musicBtn, sfxBtn;
    public Text musicTxt, sfxTxt, vibrationTxt;
    public List<Image> starGroup;
    public Image vibraToggleBg, soundToggleBg;
    public RectTransform vibraHandle, soundHandle;
    public Sprite toggleBgOn, toggleBgOff, starYell, starWhite;
    public float leftPos, rightPos;

    private int _currentRateIndex = -1;

    private bool onMusic, onSFX, onVibration;

    private void Start()
    {
        onMusic = UserData.MusicSetting;
        onSFX = UserData.SFXSetting;
        onVibration = UserData.VibrationSetting;

        privacyButton.onClick.AddListener(PrivacyOnClick);
        termsButton.onClick.AddListener(TermsOnClick);
        closeButton.onClick.AddListener(CloseOnClick);
        //closeRateButton.onClick.AddListener(CloseRateOnClick);
        vibrationBtn.onClick.AddListener(VibraClick);
        musicBtn.onClick.AddListener(MusicClick);
        sfxBtn.onClick.AddListener(SFXClick);
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
        UpdateUISound(UserData.MusicSetting);
        UpdateUIVibra(UserData.VibrationSetting);
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

    private void MusicClick()
    {
        onMusic = !onMusic;
        UserData.MusicSetting = onMusic;
        musicTxt.text = SetTextShow(SelectionSetting.Music, onMusic);
    }

    private void VibraClick()
    {
        onSFX = !onSFX;
        UserData.SFXSetting = onSFX;
        sfxTxt.text = SetTextShow(SelectionSetting.SFX, onSFX);
    }

    private void SFXClick()
    {
        onVibration = !onVibration;
        UserData.VibrationSetting = onVibration;
        vibrationTxt.text = SetTextShow(SelectionSetting.Vibration, onVibration);
    }

    private string SetTextShow(SelectionSetting selection, bool on)
    {
        string resultText = "";
        switch(selection)
        {
            case SelectionSetting.SFX:
                resultText = on ? $"{SelectionSetting.SFX} On" : $"{SelectionSetting.SFX} Off";
                break;
            case SelectionSetting.Music:
                resultText = on ? $"{SelectionSetting.Music} On" : $"{SelectionSetting.Music} Off";
                break;
            case SelectionSetting.Vibration:
                resultText = on ? $"{SelectionSetting.Vibration} On" : $"{SelectionSetting.Vibration} Off";
                break;
        }
        return resultText;
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