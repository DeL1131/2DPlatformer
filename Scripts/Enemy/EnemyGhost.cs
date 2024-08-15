using System;

public class EnemyGhost : Enemy , IDamagable
{
    public event Action<float> Damaged;

    public void TakeDamage(float damage)
    {
        _health.ChangeHealth(damage);
    }
}