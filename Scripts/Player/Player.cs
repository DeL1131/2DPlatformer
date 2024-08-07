using UnityEngine;

[RequireComponent(typeof(Movement))]

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _layerMask;

    public int Coins { get; private set; }
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public float Damage { get; private set; }
    public float AttackRange { get; private set; }

    private Movement _movement;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        Damage = _damage;
        AttackRange = _attackRange;
        _movement.Attacked += Attack;
    }

    private void Start()
    {
        CurrentHealth = _maxHealth;       
    }

    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _movement.Attacked -= Attack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPickupable iPickupable))
        {
            iPickupable.Pickup();
            Coins++;
        }
        if (collision.gameObject.TryGetComponent<HealthBox>(out HealthBox healthBox))
        {
            TakeHealingBox(healthBox.HealingAmount);
            healthBox.Pickup();
        }
    }

    private void TakeHealingBox(float amountHealing)
    {
        Debug.Log(CurrentHealth);
        CurrentHealth += amountHealing;
        Debug.Log(CurrentHealth);

        if (CurrentHealth > _maxHealth)
            CurrentHealth = _maxHealth;
        Debug.Log(CurrentHealth);

    }

    private void Attack()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, AttackRange, _layerMask);


        if (targets != null)
        {
            foreach (Collider2D target in targets)
            {
                if (target.gameObject.TryGetComponent(out IDamagable damagable))
                {
                    damagable.TakeDamage(Damage);
                    return;
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }
}