using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private List<IGoldObserver> observers = new List<IGoldObserver>(); // Lista de observadores
    private int totalCoins = 0;

    // Método para agregar un observador
    public void AddObserver(IGoldObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    // Método para remover un observador
    public void RemoveObserver(IGoldObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    // Método para notificar a los observadores
    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnCoinCollected(totalCoins);
        }
    }

    // Método que se llama cuando se recoge una moneda
    public void CollectCoin()
    {
        totalCoins++;
        NotifyObservers();
    }
}
