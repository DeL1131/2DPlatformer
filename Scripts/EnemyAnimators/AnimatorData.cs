using UnityEngine;

public static class AnimatorData
{
    public static class Params
    {
        public static readonly int HeroJump = Animator.StringToHash(nameof(HeroJump));
        public static readonly int HeroRun = Animator.StringToHash(nameof(HeroRun));
        public static readonly int HeroAttack = Animator.StringToHash(nameof(HeroAttack));
        public static readonly int HeroFly = Animator.StringToHash(nameof(HeroFly));
        public static readonly int ZombieDeathAnimation = Animator.StringToHash(nameof(ZombieDeathAnimation));
        public static readonly int ZombieHitAnimation = Animator.StringToHash(nameof(ZombieHitAnimation));
        public static readonly int ZombieAttackAnimation = Animator.StringToHash(nameof(ZombieAttackAnimation));
        public static readonly int SkeletonAttackAnimation = Animator.StringToHash(nameof(SkeletonAttackAnimation));
        public static readonly int SkeletonDeathAnimation = Animator.StringToHash(nameof(SkeletonDeathAnimation));
        public static readonly int SkeletonHitAnimation = Animator.StringToHash(nameof(SkeletonHitAnimation));
    }
}