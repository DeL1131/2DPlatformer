using System;

public class EnemySkeleton : Enemy , IDamagable
{
    public event Action<float> Damaged;

    public void TakeDamage(float damage)
    {
        _health.ChangeHealth(damage);
    }
}