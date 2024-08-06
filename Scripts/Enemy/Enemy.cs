using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _maxHeallth;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected LayerMask _layerMask;

    public float Speed { get; private set; }
    public float CurrentHealth { get; protected set; }
    public float Damage { get; private set; }
    public float AttackRange { get; private set; }

    public event Action Died;
    public event Action Attacked;

    private void Awake()
    {
        Speed = _speed;
        CurrentHealth = _maxHeallth;
        Damage = _damage;
        AttackRange = _attackRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            if (_layerMask == (_layerMask | (1 << collision.gameObject.layer)))
                Attack(damagable);
        }
    }

    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            Died?.Invoke();
            StartCoroutine(EnemyDied());
        }
    }

    private void Attack(IDamagable damagable)
    {
        Attacked?.Invoke();
        damagable.TakeDamage(Damage);
    }

    private IEnumerator EnemyDied()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);        
    }
}