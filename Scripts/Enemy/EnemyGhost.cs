public class EnemyGhost : Enemy , IDamagable
{
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }
}