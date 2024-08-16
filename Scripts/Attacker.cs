using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _attackInterval;

    private float _attackCooldown = 0.0f;

    public event Action Attacked;

    public float AttackCooldown => _attackCooldown;

    private void FixedUpdate()
    {
        _attackCooldown -= Time.fixedDeltaTime;
    }

    public void Attack(LayerMask layerMask, float attackRange, float damage)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attackRange, layerMask);

        if (targets != null)
        {
            foreach (Collider2D target in targets)
            {
                if (_attackCooldown <= 0)
                {
                    if (layerMask == (layerMask | (1 << target.gameObject.layer)) && target.gameObject.TryGetComponent(out IDamagable damagable))
                    {
                        Attacked?.Invoke();
                        damagable.TakeDamage(damage);
                        _attackCooldown = _attackInterval;
                        return;
                    }
                }
            }
        }
    }
}