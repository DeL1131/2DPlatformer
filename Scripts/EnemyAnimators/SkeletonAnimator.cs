using UnityEngine;

public class SkeletonAnimator : EnemyAnimator
{
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