using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]

public abstract class EnemyAnimator : MonoBehaviour
{
    protected Animator Animator;
    protected Enemy Enemy;

    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        Animator = GetComponent<Animator>();
        Enemy.Died += PlayEnemyDeathAnimation;
        Enemy.Attacked += PlayEnemyAttackAnimation;
    }

    private void OnDestroy()
    {
        Enemy.Died -= PlayEnemyDeathAnimation;
        Enemy.Attacked -= PlayEnemyAttackAnimation;
    }

    protected abstract void PlayEnemyDeathAnimation();

    protected abstract void PlayEnemyAttackAnimation();
}
