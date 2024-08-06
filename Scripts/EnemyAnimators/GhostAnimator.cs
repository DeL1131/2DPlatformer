using UnityEngine;

public class GhostAnimator : EnemyAnimator
{
    protected override void PlayEnemyDeathAnimation()
    {
        Animator.Play(AnimatorData.Params.GhostDeathAnimation);
    }

    protected override void PlayEnemyAttackAnimation()
    {
        Animator.Play(AnimatorData.Params.GhostAttackAnimation);
    }
}