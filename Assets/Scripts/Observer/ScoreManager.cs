using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    private void OnEnable()
    {
        // Suscribirse al evento de recolección de monedas
        CoinCollector.OnCoinCollected += UpdateScore;
    }

    private void OnDisable()
    {
        // Cancelar la suscripción para evitar problemas de memoria
        CoinCollector.OnCoinCollected -= UpdateScore;
    }

    private void UpdateScore(int coinCount)
    {
        score = coinCount * 10; // Cada moneda vale 10 puntos
        Debug.Log("Puntuación actual: " + score);
    }
}
