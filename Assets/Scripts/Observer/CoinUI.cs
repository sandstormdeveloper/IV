using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Se mete en el texto de las monedas
public class CoinUI : MonoBehaviour, IGoldObserver
{
    [SerializeField] private TextMeshProUGUI coinText; //Serialize para q las variables aparezcan en el editor aunq sean privadas

    public void OnCoinCollected(int totalCoins)
    {
        coinText.text = "Monedas: " + totalCoins; // Actualiza la interfaz con el nº de monedas
    }
}
