using System;
using UnityEngine;

public class Coin : MonoBehaviour , IPickupable
{
    public event Action<Coin> PickedUp;

    public void Pickup()
    {
        PickedUp?.Invoke(this);
    }
}