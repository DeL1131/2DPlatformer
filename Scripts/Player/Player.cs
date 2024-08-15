using System;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent (typeof(Health))]
[RequireComponent(typeof(CoinPickUp))]
[RequireComponent(typeof(HealthBoxPickUp))]


public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _layerMask;

    private InputManager _inputManager;
    private Health _health;
    private CoinPickUp _coinPickUp;
    private HealthBoxPickUp _healthBoxPickUp;

    public event Action<float> Damaged;

    public LayerMask LayerMask => _layerMask;
    public int Coins { get; private set; }
    public float Damage { get; private set; }
    public float AttackRange { get; private set; }

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _health = GetComponent<Health>();
        _coinPickUp = GetComponent<CoinPickUp>();
        _healthBoxPickUp = GetComponent<HealthBoxPickUp>();

        _health.HealhChanged += ChackHealth;
        _coinPickUp.PickUp += ChangeCoinAmaunt;
        _healthBoxPickUp.PickUp += Healing;

        Damage = _damage;
        AttackRange = _attackRange;
    }

    private void OnDestroy()
    {
        _health.HealhChanged -= ChackHealth;
        _coinPickUp.PickUp -= ChangeCoinAmaunt;
        _healthBoxPickUp.PickUp -= Healing;
    }

    public void TakeDamage(float damage)
    {        
        Damaged?.Invoke(damage);
    }

    private void ChackHealth(float currentHealth)
    {
        if (_health.CurrentHealth <= 0)
        {
            Destroy(gameObject);          
        }
    }

    private void ChangeCoinAmaunt()
    {
        Coins++;
    }
    
    private void Healing(float healAmount)
    {
        _health.Healing(healAmount);
    }
}