using System;
using UnityEngine;

[RequireComponent(typeof(IDamagable))]

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private IDamagable _damagable;

    public event Action<float> HealhChanged;

    public float CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = _maxHealth;

        if (gameObject.TryGetComponent(out IDamagable damagable))
            _damagable = damagable;

        _damagable.Damaged += ChangeHealth;
    }

    private void OnDisable()
    {
        _damagable.Damaged -= ChangeHealth;
    }

    public void ChangeHealth(float damage)
    {
        if (damage > 0)
            CurrentHealth -= damage;

        HealhChanged?.Invoke(CurrentHealth);
    }

    public void Healing(float healAmount)
    {
        CurrentHealth += healAmount;

        if (CurrentHealth > _maxHealth)
            CurrentHealth = _maxHealth;
    }
}