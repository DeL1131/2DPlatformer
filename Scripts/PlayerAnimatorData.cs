using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int HeroRun = Animator.StringToHash(nameof(HeroRun));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}