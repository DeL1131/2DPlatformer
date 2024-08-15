using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent (typeof(EnemyAttack))]

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float Damage;    

    protected Health _health;
    private EnemyAttack _enemyAttack;
    private float _deathDelay = 0.3f;    
    private bool _isHaveTrigger;

    public event Action Died;
    public event Action Attacked;

    public float CurrentDamage { get; private set; }

    private void Awake()
    {
        _health = GetComponent<Health>();
        _enemyAttack = GetComponent<EnemyAttack>();

        CurrentDamage = Damage;
        _health.HealhChanged += ChackHealth;
        _enemyAttack.Attacked += Attack;
    }

    private void OnDisable()
    {
        _health.HealhChanged -= ChackHealth;
        _enemyAttack.Attacked -= Attack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            _isHaveTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            _isHaveTrigger = false;
        }
    }

    private void Attack(IDamagable damagable)
    {
        Attacked?.Invoke();
        damagable.TakeDamage(CurrentDamage);
    }

    private void ChackHealth(float currentHealth)
    {
        if (_health.CurrentHealth <= 0)
        {
            Died?.Invoke();
            StartCoroutine(StartEnemyDeath());
        }
    }

    private IEnumerator StartEnemyDeath()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_deathDelay);

        yield return waitForSeconds;
        Destroy(gameObject);
    }
}