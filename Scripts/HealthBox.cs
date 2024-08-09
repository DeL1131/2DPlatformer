using UnityEngine;

public class HealthBox : MonoBehaviour, IPickupable
{
    [SerializeField] private float Healing;
    public float HealingAmount { get; private set; }

    private void Awake()
    {
        HealingAmount = Healing;
    }

    public void Pickup()
    {
        Destroy(gameObject);
    }
}