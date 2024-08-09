using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float MaxHeallth;
    [SerializeField] protected float Damage;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected LayerMask LayerMask;
    [SerializeField] private float attackCooldown = 1.0f;

    private float deathDelay = 0.3f;
    private bool _isHaveTrigger;

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

    private void Update()
    {
        if (_isHaveTrigger)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, AttackRange, LayerMask);

            foreach (Collider2D target in targets)
            {
                if (LayerMask == (LayerMask | (1 << target.gameObject.layer)) && target.gameObject.TryGetComponent(out IDamagable damagable))
                {
                    Attack(damagable);
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
        WaitForSeconds waitForSeconds = new WaitForSeconds(deathDelay);

        yield return waitForSeconds;
        Destroy(gameObject);
    }
}