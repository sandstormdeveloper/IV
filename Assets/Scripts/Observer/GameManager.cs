using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
       [SerializeField] private CoinCollector coinCollector;
       [SerializeField] private CoinUI coinUI;

       private void Start()
        {
            // Registra los observadores
            coinCollector.AddObserver(coinUI);
        }
}
