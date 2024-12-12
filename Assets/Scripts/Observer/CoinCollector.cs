using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private List<IGoldObserver> observers = new List<IGoldObserver>(); // Lista de observadores
    private int totalCoins = 0;

    // M�todo para agregar un observador
    public void AddObserver(IGoldObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    // M�todo para remover un observador
    public void RemoveObserver(IGoldObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    // M�todo para notificar a los observadores
    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnCoinCollected(totalCoins);
        }
    }

    // M�todo que se llama cuando se recoge una moneda
    public void CollectCoin()
    {
        totalCoins++;
        NotifyObservers();
    }
}
