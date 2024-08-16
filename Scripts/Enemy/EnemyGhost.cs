using System;

public class EnemyGhost : Enemy , IDamagable
{
    public event Action<float> Damaged;

    public void TakeDamage(float damage)
    {
        Health.DamageHealth(damage);
    }
}