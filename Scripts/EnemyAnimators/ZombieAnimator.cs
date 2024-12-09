using UnityEngine;

[RequireComponent (typeof(EnemyZombie))]

public class ZombieAnimator : EnemyAnimator
{
    private EnemyZombie _enemyZombie;

    private void OnEnable()
    {
        _enemyZombie = GetComponent<EnemyZombie>();
        _enemyZombie.Hited += PlayEnemyHitAnimation;
    }

    private void OnDisable()
    {
        _enemyZombie.Hited -= PlayEnemyHitAnimation;
    }

    protected override void PlayEnemyDeathAnimation()
    {
        Animator.Play(AnimatorData.Params.ZombieDeathAnimation);
    }

    protected override void PlayEnemyAttackAnimation()
    {
        Animator.Play(AnimatorData.Params.ZombieAttackAnimation);
    }

    protected override void PlayEnemyHitAnimation()
    {
        Animator.Play(AnimatorData.Params.ZombieHitAnimation);
    }
}