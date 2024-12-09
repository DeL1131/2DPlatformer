using System;

public class EnemyZombie : Enemy , IDamagable
{
    public event Action<float> Damaged;
    public event Action Hited;

    public void TakeDamage(float damage)
    {
        Hited?.Invoke();
        Health.DamageHealth(damage);
    }
}