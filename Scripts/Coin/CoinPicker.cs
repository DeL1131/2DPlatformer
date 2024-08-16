using System;
using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    public event Action PickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPickupable iPickupable))
        {
            PickUp?.Invoke();
            iPickupable.Pickup();           
        }
    }
}
