using UnityEngine;

public static class AnimatorData
{
    public static class Params
    {
        public static readonly int HeroJump = Animator.StringToHash(nameof(HeroJump));
        public static readonly int HeroRun = Animator.StringToHash(nameof(HeroRun));
        public static readonly int HeroAttack = Animator.StringToHash(nameof(HeroAttack));
        public static readonly int HeroFly = Animator.StringToHash(nameof(HeroFly));
        public static readonly int GhostDeathAnimation = Animator.StringToHash(nameof(GhostDeathAnimation));
        public static readonly int GhostAttackAnimation = Animator.StringToHash(nameof(GhostAttackAnimation));
        public static readonly int SkeletonAttackAnimation = Animator.StringToHash(nameof(SkeletonAttackAnimation));
        public static readonly int SkeletonDeathAnimation = Animator.StringToHash(nameof(SkeletonDeathAnimation));
        public static readonly int SkeletonHitAnimation = Animator.StringToHash(nameof(SkeletonHitAnimation));

    }
}