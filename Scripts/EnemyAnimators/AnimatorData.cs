using UnityEngine;

public static class AnimatorData
{
    public static class Params
    {
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int HeroRun = Animator.StringToHash(nameof(HeroRun));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int GhostDeathAnimation = Animator.StringToHash(nameof(GhostDeathAnimation));
        public static readonly int GhostAttackAnimation = Animator.StringToHash(nameof(GhostAttackAnimation));
        public static readonly int SkeletonAttackAnimation = Animator.StringToHash(nameof(SkeletonAttackAnimation));
        public static readonly int SkeletonDeathAnimation = Animator.StringToHash(nameof(SkeletonDeathAnimation));
    }
}