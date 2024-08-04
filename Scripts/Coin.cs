using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> CoinPickedUp;

    public void ReturnToPool()
    {
        CoinPickedUp?.Invoke(this);
    }
}