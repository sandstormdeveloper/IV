using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinCollector collector = other.GetComponent<CoinCollector>();
            if (collector != null)
            {
                collector.CollectCoin(); // Notificar al sistema que se recogió una moneda
                Destroy(gameObject); // Destruir la moneda
            }
        }
    }
    
}
