using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public List<Image> starGroup;
    public Image vibraToggleBg, soundToggleBg;
    public RectTransform vibraHandle, soundHandle;
    public Sprite toggleBgOn, toggleBgOff, starYell, starWhite;
    public float leftPos, rightPos;

    public Button vibrationBtn, musicBtn, sfxBtn;
    public TextMeshProUGUI musicTxt, sfxTxt, vibrationTxt;
    public Image musicIcon, sfxIcon, vibrationIcon;

    private float alphaIconOff = 150f;
    private bool onMusic, onSFX, onVibration;

    private int _currentRateIndex = -1;
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
        UpdateUIElement(UserData.MusicSetting, musicIcon, musicTxt);
        UpdateUIElement(UserData.VibrationSetting, vibrationIcon, vibrationTxt);
        UpdateUIElement(UserData.SFXSetting, sfxIcon, sfxTxt);
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

    private void UpdateUIElement(bool isOn, Image icon, TextMeshProUGUI txt)
    {
        if (isOn)
        {
            icon.color = new Color(0, 0, 0, 255);
            txt.color = new Color(0, 0, 0, 255);
        }
        else
        {
            icon.color = new Color(0, 0, 0, alphaIconOff / 255f);
            txt.color = new Color(0, 0, 0, alphaIconOff / 255f);
        }
    }


    private void MusicClick()
    {
        onMusic = !onMusic;
        UserData.MusicSetting = onMusic;
        musicTxt.text = SetTextShow(SelectionSetting.Music, onMusic);
        EventDispatcher.Instance.PostEvent(EventID.OnMusicChangeValue, onMusic);
        UpdateUIElement(onMusic, musicIcon, musicTxt);
    }

    private void VibraClick()
    {
        onVibration = !onVibration;
        UserData.VibrationSetting = onVibration;
        vibrationTxt.text = SetTextShow(SelectionSetting.Vibration, onVibration);
        
        UpdateUIElement(onVibration, vibrationIcon, vibrationTxt);
    }

    private void SFXClick()
    {
        onSFX = !onSFX;
        UserData.SFXSetting = onSFX;
        sfxTxt.text = SetTextShow(SelectionSetting.SFX, onSFX);
        EventDispatcher.Instance.PostEvent(EventID.OnSoundChangeValue, onSFX);
        UpdateUIElement(onSFX, sfxIcon, sfxTxt);
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