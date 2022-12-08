using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleUI : MonoBehaviour
{
    [SerializeField] Button mainMenuButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }

    private void OnMainMenuClicked()
    {
        UIManager.Instance.Loading(() =>
        {
            Hide();
            GameManager.Instance.ReturnMainMenu();
            UIManager.Instance.ShowMainMenuUI();
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);    
    }
    
}
