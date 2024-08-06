using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [SerializeField] private float Healing;
    public float HealingAmount { get; private set; }

    private void Awake()
    {
        HealingAmount = Healing;
    }

    public void DestroyHealthBox()
    {
        Destroy(gameObject);
    }
}
