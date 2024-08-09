public class EnemySkeleton : Enemy , IDamagable
{
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }
}