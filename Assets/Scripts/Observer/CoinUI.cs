using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour, IGoldObserver
{
    [SerializeField] private TextMeshProUGUI coinText;

    public void OnCoinCollected(int totalCoins)
    {
        coinText.text = "Monedas: " + totalCoins; // Actualiza la interfaz
    }
}
