using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Parte del personaje
public class CoinCollector : MonoBehaviour
{
    private List<IGoldObserver> observers = new List<IGoldObserver>(); // Lista de observadores
    private int totalCoins = 0;

    // Agregar un observador
    public void AddObserver(IGoldObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    // Eliminar un observador
    public void RemoveObserver(IGoldObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    // Notificar a los observadores
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
