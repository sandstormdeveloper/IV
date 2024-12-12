using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    private void OnEnable()
    {
        // Suscribirse al evento de recolecci�n de monedas
        CoinCollector.OnCoinCollected += UpdateScore;
    }

    private void OnDisable()
    {
        // Cancelar la suscripci�n para evitar problemas de memoria
        CoinCollector.OnCoinCollected -= UpdateScore;
    }

    private void UpdateScore(int coinCount)
    {
        score = coinCount * 10; // Cada moneda vale 10 puntos
        Debug.Log("Puntuaci�n actual: " + score);
    }
}
