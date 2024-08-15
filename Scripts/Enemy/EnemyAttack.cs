using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected float AttackInterval;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected LayerMask LayerMask;

    private Enemy _enemy;
    private float _attackCooldown = 0.0f;

    public event Action<IDamagable> Attacked;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        _attackCooldown -= Time.fixedDeltaTime;
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
                    Attacked?.Invoke(damagable);
                    _attackCooldown = AttackInterval;
                }
            }
        }
    }
}