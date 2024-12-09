using UnityEngine;

[RequireComponent(typeof(EnemySkeleton))]

public class SkeletonAnimator : EnemyAnimator
{
    private EnemySkeleton _enemySkeleton;

    private void OnEnable()
    {
        _enemySkeleton = GetComponent<EnemySkeleton>();
        _enemySkeleton.Hited += PlayEnemyHitAnimation;
    }

    private void OnDisable()
    {
        _enemySkeleton.Hited -= PlayEnemyHitAnimation;
    }

    protected override void PlayEnemyDeathAnimation()
    {
        Animator.Play(AnimatorData.Params.SkeletonDeathAnimation);
    }

    protected override void PlayEnemyAttackAnimation()
    {
        Animator.Play(AnimatorData.Params.SkeletonAttackAnimation);
    }

    protected override void PlayEnemyHitAnimation()
    {
        Animator.Play(AnimatorData.Params.SkeletonHitAnimation);
    }
}