using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    // Evento que notifica la recolecci�n de una moneda
    public static event Action<int> OnCoinCollected;

    private int coinCount = 0;

    // M�todo para recolectar monedas
    public void CollectCoin()
    {
        coinCount++;
        // Notifica a los observadores
        OnCoinCollected?.Invoke(coinCount);
    }
}
