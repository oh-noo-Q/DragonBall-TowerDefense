using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] Button skipButton;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button mainMenuButton;

    private void Awake()
    {
        skipButton.onClick.AddListener(OnSkipClicked);
        tryAgainButton.onClick.AddListener(OnTryAgainClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnEnable()
    {
        SetButtonInteractable(true);
    }

    private void SetButtonInteractable(bool interactable)
    {
        tryAgainButton.interactable = interactable;
        skipButton.interactable = interactable;
        mainMenuButton.interactable = interactable;
    }

    private void OnMainMenuButtonClicked()
    {
        SetButtonInteractable(false);
        UIManager.Instance.Loading(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.ShowMainMenuUI();
        });
    }

    private void OnTryAgainClicked()
    {
        SetButtonInteractable(false);
        UIManager.Instance.Loading(() =>
        {
            gameObject.SetActive(false);
            this.PostEvent(EventID.OnLoadLevel);
        },
        () => {
            GameManager.Instance.GameIntro();
        });
    }

    private void OnSkipClicked()
    {
        SetButtonInteractable(false); 
        UserData.CurrentLevel++;
        UIManager.Instance.Loading(() =>
        {
            gameObject.SetActive(false);
            this.PostEvent(EventID.OnLoadLevel);
        },
        () => {
            GameManager.Instance.GameIntro();
        });
    }
}
