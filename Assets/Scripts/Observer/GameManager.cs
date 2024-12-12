using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Se mete en el objeto vacío GameManager para inicializar las cosas del patron observer
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
