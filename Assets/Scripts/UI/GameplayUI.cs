using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button x2Button;
    [SerializeField] Button skipButton;
    [SerializeField] Button radaBtn;
    [SerializeField] Button peanutBtn;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Text radaText;
    [SerializeField] Text peanutText;
    [SerializeField] GameObject RevivePopup;
    [SerializeField] GameObject MergePopup;
    private void Awake()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        skipButton.onClick.AddListener(OnSkipButtonClicked);
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        EventDispatcher.Instance.RegisterListener(EventID.OnUpdateRada, UpdateRadaText);
        EventDispatcher.Instance.RegisterListener(EventID.OnUpdatePeanut, UpdatePeanutText);
        EventDispatcher.Instance.RegisterListener(EventID.OnShowRevive, ShowRevivePopup);
        EventDispatcher.Instance.RegisterListener(EventID.OnShowMerge, ShowMergePopup);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener(EventID.OnUpdateRada, UpdateRadaText);
        EventDispatcher.Instance.RemoveListener(EventID.OnUpdatePeanut, UpdatePeanutText);
        EventDispatcher.Instance.RemoveListener(EventID.OnShowRevive, ShowRevivePopup);
        EventDispatcher.Instance.RemoveListener(EventID.OnShowMerge, ShowMergePopup);
    }

    private void OnEnable()
    {
        levelText.text = "Level " + UserData.CurrentLevel.ToString();
        ShowButtons(false);
        UpdateRadaText(null);
        UpdatePeanutText(null);
        //SetButtonsInteractable(true);
    }

    public void ShowButtons(bool show)
    {
        mainMenuButton.gameObject.SetActive(show);
        restartButton.gameObject.SetActive(show);
        skipButton.gameObject.SetActive(show);
        x2Button.gameObject.SetActive(show);
        radaBtn.gameObject.SetActive(show);
        //peanutBtn.gameObject.SetActive(show);
    }

    public void SetButtonsInteractable(bool interactable)
    {
        mainMenuButton.interactable = interactable;
        restartButton.interactable = interactable;
        skipButton.interactable = interactable;
        x2Button.interactable = interactable;
        radaBtn.interactable = interactable;
    }

    private void OnMainMenuClicked()
    {
        ShowButtons(false);
        UIManager.Instance.Loading(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.ShowMainMenuUI();
        }, 1f);
    }

    private void OnSkipButtonClicked()
    {
        ShowButtons(false);
        //UserData.CurrentLevel++;
        this.PostEvent(EventID.OnWinLevel);
    }

    private void OnRestartButtonClicked()
    {
        ShowButtons(false);
        GameManager.Instance.GameMode = GameMode.AUTOPLAY;
        UIManager.Instance.Loading(() =>
        {
            this.PostEvent(EventID.OnLoadLevel);
        }, 2f, 
        () => {
            GameManager.Instance.GameIntro();
        });
    }

    void UpdateRadaText(object obj)
    {
        radaText.text = $"{UserData.NumberRada}";
    }

    void UpdatePeanutText(object obj)
    {
        peanutText.text = $"{UserData.NumberPeanut}";
    }

    public void OnRadaBtnClicked()
    {
        if(UserData.NumberRada > 0)
            GameManager.Instance.ShowAllStrength();
        else
        {

        }
    }

    public void ShowRevivePopup(object obj)
    {
        RevivePopup.SetActive(true);
    }

    public void HideRevivePopup()
    {
        RevivePopup.SetActive(false);
    }

    public void OnClickRevive()
    {
        GameManager.Instance.Player.Revive();
        UserData.NumberPeanut--;
        UpdatePeanutText(null);
        HideRevivePopup();
    }

    public void OnClickUnrevive()
    {
        EventDispatcher.Instance.PostEvent(EventID.OnLoseLevel);
        HideRevivePopup();
    }

    public void ShowMergePopup(object obj)
    {
        MergePopup.SetActive(true);
    }
    public void HideMergePopup()
    {
        MergePopup.SetActive(false);
    }

    public void OnClickMerge(int value)
    {
        GameManager.Instance.Player.Merge(value);
        HideMergePopup();
    }
}
