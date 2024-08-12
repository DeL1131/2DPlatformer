using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float MaxHeallth;
    [SerializeField] protected float Damage;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected LayerMask LayerMask;
    [SerializeField] protected float AttackInterval;

    private float _deathDelay = 0.3f;
    private bool _isHaveTrigger;

    private float _attackCooldown = 0.0f;

    public event Action Died;
    public event Action Attacked;

    public float CurrentHealth { get; protected set; }
    public float CurrentDamage { get; private set; }
    public float CurrentAttackRange { get; private set; }

    private void Awake()
    {
        CurrentHealth = MaxHeallth;
        CurrentDamage = Damage;
        CurrentAttackRange = AttackRange;
    }

    private void FixedUpdate()
    {
        _attackCooldown -= Time.fixedDeltaTime;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, AttackRange, LayerMask);

        foreach (Collider2D target in targets)
        {
            if (LayerMask == (LayerMask | (1 << target.gameObject.layer)) && target.gameObject.TryGetComponent(out IDamagable damagable))
            {
                if (_attackCooldown <= 0)
                {
                    Attack(damagable);
                    _attackCooldown = AttackInterval;
                }
            }
        }

        if (CurrentHealth <= 0)
        {
            Died?.Invoke();
            StartCoroutine(StartEnemyDeath());
        }
    }

    private void Attack(IDamagable damagable)
    {
        Attacked?.Invoke();
        damagable.TakeDamage(CurrentDamage);
    }

    private IEnumerator StartEnemyDeath()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_deathDelay);

        yield return waitForSeconds;
        Destroy(gameObject);
    }
}