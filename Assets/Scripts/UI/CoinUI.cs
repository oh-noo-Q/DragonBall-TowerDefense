using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    private void OnEnable()
    {
        UpdateCoinText();
        this.RegisterListener(EventID.OnUpdateCoin, UpdateCoinText);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.OnUpdateCoin, UpdateCoinText);
    }

    private void UpdateCoinText(object param = null) {
        coinText.text = UserData.CurrentCoin.ToString();
    }
}
