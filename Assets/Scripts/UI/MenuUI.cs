using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button settingButton;
    [SerializeField] Button castleButton;
    [SerializeField] Button shopButton;
    [SerializeField] TextMeshProUGUI levelText;

    private void Awake()
    {
        playButton.onClick.AddListener(Play);
        settingButton.onClick.AddListener(ShowSettingPanel);
        castleButton.onClick.AddListener(JoinCastle);
        shopButton.onClick.AddListener(JoinShop);
    }

    private void OnEnable()
    {
        playButton.interactable = true;
        levelText.text = "Level " + UserData.CurrentLevel.ToString();
    }

    private void ShowSettingPanel()
    {
        UIManager.Instance.ShowSettingPanel();
    }

    private void Play()
    {
        playButton.interactable = false;
        UIManager.Instance.Loading(() =>
        {
            gameObject.SetActive(false);
            this.PostEvent(EventID.OnLoadLevel);
        }, 2f,
        () => {
            GameManager.Instance.GameIntro();
        });
    }

    private void JoinCastle()
    {
        UIManager.Instance.Loading(() =>
        {
            gameObject.SetActive(false);
            GameManager.Instance.JoinCastle();
        }, 1f);
    }

    private void JoinShop()
    {
        UIManager.Instance.Loading(() =>
        {
            gameObject.SetActive(false);
            GameManager.Instance.ShowShop();
        }, 1f);
    }
}
