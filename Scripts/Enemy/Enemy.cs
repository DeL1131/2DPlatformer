using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] protected float MaxHeallth;
    [SerializeField] protected float Damage;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected LayerMask LayerMask;

    public float CurrendSpeed { get; private set; }
    public float CurrentHealth { get; protected set; }
    public float CurrentDamage { get; private set; }
    public float CurrentAttackRange { get; private set; }

    public event Action Died;
    public event Action Attacked;

    private void Awake()
    {
        CurrendSpeed = CurrendSpeed;
        CurrentHealth = MaxHeallth;
        CurrentDamage = Damage;
        CurrentAttackRange = AttackRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            if (LayerMask == (LayerMask | (1 << collision.gameObject.layer)))
                Attack(damagable);
        }
    }

    private void Update()
    {
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
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);        
    }
}