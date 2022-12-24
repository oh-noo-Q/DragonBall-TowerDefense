using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] MenuUI menuUI;
    [SerializeField] WinUI winUI;
    [SerializeField] LoseUI loseUI;
    [SerializeField] GameplayUI gameplayUI;
    [SerializeField] SettingPanel setting;
    [SerializeField] CanvasGroup loading;
    [SerializeField] CastleUI castleUI;
    [SerializeField] ShopUI shopUI;

    public MenuUI MenuUI => menuUI;
    public GameplayUI GameplayUI => gameplayUI;
    private void Awake()
    {
        this.RegisterListener(EventID.OnWinLevel, OnStartShowWinUI);
        this.RegisterListener(EventID.OnLoseLevel, OnStartShowLoseUI);
        this.RegisterListener(EventID.OnLoadLevel, ShowGameplayUI);
    }

    public override void OnDestroy()
    {
        this.RemoveListener(EventID.OnWinLevel, OnStartShowWinUI);
        this.RemoveListener(EventID.OnLoseLevel, OnStartShowLoseUI);
        this.RemoveListener(EventID.OnLoadLevel, ShowGameplayUI);
    }

    private void OnStartShowLoseUI(object param)
    {
        gameplayUI.ShowButtons(false);
        StartCoroutine(ShowLoseUI());
    }

    private void OnStartShowWinUI(object param)
    {
        gameplayUI.ShowButtons(false);
        StartCoroutine(ShowWinUI());
    }

    private void ShowGameplayUI(object param)
    {
        gameplayUI.gameObject.SetActive(true);
    }

    public void ShowSettingPanel()
    {
        setting.gameObject.SetActive(true);
    }

    public void ShowMainMenuUI()
    {
        menuUI.gameObject.SetActive(true);
    }

    private IEnumerator ShowLoseUI()
    {
        yield return ExtensionClass.GetWaitForSeconds(2f);
        loseUI.gameObject.SetActive(true);
        gameplayUI.gameObject.SetActive(false);
    }

    private IEnumerator ShowWinUI()
    {
        yield return ExtensionClass.GetWaitForSeconds(2f);
        winUI.gameObject.SetActive(true);
        gameplayUI.gameObject.SetActive(false);
    }

    public void Loading(Action loadAction = null, float time = 2f, Action callback = null)
    {
        loading.interactable = true;
        loading.DOFade(1f, 0.5f).OnComplete(() =>
        {
            loadAction?.Invoke();
            loading.DOFade(0f, time).OnComplete(() =>
            {
                loading.interactable = false;
                callback?.Invoke();
            });
        });
    }

    public void ShowCastleUI()
    {
        castleUI.Show();
    }

    public void ShowShopUI()
    {
        shopUI.Show();
    }
}
