using UnityEngine;

public class HealthBox : MonoBehaviour, IPickupable
{
    [SerializeField] private float _healing;
    public float HealingAmount => _healing;

    public void Pickup()
    {
        Destroy(gameObject);
    }
}