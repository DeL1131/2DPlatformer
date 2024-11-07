using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent (typeof(Health))]
[RequireComponent(typeof(CoinPicker))]
[RequireComponent(typeof(HealthBoxPicker))]
[RequireComponent(typeof(Attacker))]

public class Player : MonoBehaviour, IDamagable, IHealeble
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _layerMask;

    private Attacker _attacker;
    private PlayerInput _playerInput;
    private Health _health;
    private CoinPicker _coinPickUp;
    private HealthBoxPicker _healthBoxPickUp;

    public event Action<float> Damaged;
    public event Action<float> HealthChanged;

    public LayerMask LayerMask => _layerMask;
    public int Coins { get; private set; }
    public float Damage { get; private set; }
    public float AttackRange { get; private set; }

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _playerInput = GetComponent<PlayerInput>();
        _health = GetComponent<Health>();
        _coinPickUp = GetComponent<CoinPicker>();
        _healthBoxPickUp = GetComponent<HealthBoxPicker>();

        _playerInput.Mouse0Pressed += Attack;
        _health.HealhChanged += VerifyHealth;
        _coinPickUp.PickUp += ChangeCoinAmaunt;
        _healthBoxPickUp.PickUp += Healing;

        Damage = _damage;
        AttackRange = _attackRange;
    }

    private void OnDestroy()
    {
        _health.HealhChanged -= VerifyHealth;
        _coinPickUp.PickUp -= ChangeCoinAmaunt;
        _healthBoxPickUp.PickUp -= Healing;
    }

    public void TakeDamage(float damage)
    {        
        Damaged?.Invoke(damage);
    }

    private void VerifyHealth(float currentHealth)
    {
        HealthChanged?.Invoke(currentHealth);

        if (_health.CurrentHealth <= 0)
        {
            Destroy(gameObject);          
        }
    }

    private void ChangeCoinAmaunt()
    {
        Coins++;
    }
    
    public void Healing(float healAmount)
    {
        _health.Healing(healAmount);
    }

    private void Attack()
    {
        if (_attacker.AttackCooldown <= 0)
            _attacker.Attack(LayerMask, AttackRange, Damage);
    }
}