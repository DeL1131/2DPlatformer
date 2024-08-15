using System;
using UnityEngine;

public class HealthBoxPickUp : MonoBehaviour
{
    public event Action<float> PickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<HealthBox>(out HealthBox healthBox))
        {
            PickUp?.Invoke(healthBox.HealingAmount);
            healthBox.Pickup();
        }
    }
}