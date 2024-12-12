using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    // Evento que notifica la recolección de una moneda
    public static event Action<int> OnCoinCollected;

    private int coinCount = 0;

    // Método para recolectar monedas
    public void CollectCoin()
    {
        coinCount++;
        // Notifica a los observadores
        OnCoinCollected?.Invoke(coinCount);
    }
}
