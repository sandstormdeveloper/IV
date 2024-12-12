using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoldObserver {
    void OnCoinCollected(int totalCoins);
}