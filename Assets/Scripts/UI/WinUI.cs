using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] Button x5Button;
    [SerializeField] Button noThanksButton;
    [SerializeField] Button mainMenuButton;

    private void Awake()
    {
        noThanksButton.onClick.AddListener(OnNoThanksClicked);
        x5Button.onClick.AddListener(OnX5ButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnEnable()
    {
        SetButtonInteractable(true);
    }

    private void SetButtonInteractable(bool interactable)
    {
        noThanksButton.interactable = interactable;
        x5Button.interactable = interactable;
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

    private void OnX5ButtonClicked()
    {
        SetButtonInteractable(false);
        UIManager.Instance.Loading(() =>
        {
            gameObject.SetActive(false);
            UserData.CurrentCoin += 100;
            this.PostEvent(EventID.OnLoadLevel);
        }, 2f,
        () => {
            GameManager.Instance.GameIntro();
        });
    }

    private void OnNoThanksClicked()
    {
        SetButtonInteractable(false);
        UIManager.Instance.Loading(() =>
        {
            gameObject.SetActive(false);
            UserData.CurrentCoin += 100;
            this.PostEvent(EventID.OnLoadLevel);
        }, 2f,
        () => {
            GameManager.Instance.GameIntro();
        });
    }
}
