using System;

public class EnemySkeleton : Enemy , IDamagable
{
    public event Action<float> Damaged;

    public void TakeDamage(float damage)
    {
        Health.DamageHealth(damage);
    }
}