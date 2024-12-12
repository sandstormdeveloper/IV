using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) //Al tocar al jugador...
    {
        if (other.CompareTag("Player"))
        {
            CoinCollector collector = other.GetComponent<CoinCollector>(); //"enlazamos" a coinCollector
            if (collector != null)
            {
                collector.CollectCoin(); // Manda mensaje de que se recogió una moneda
                Destroy(gameObject); // Destruye la moneda
            }
        }
    }
    
}
