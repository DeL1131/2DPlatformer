using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(Movement))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Movement _movement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        _movement.Jumped += PlayJumpAnimation;
        _movement.Attacked += PlayAttackAnimation;
        _movement.Running += PlayerRunningAnimation;
    }

    private void OnDisable()
    {
        _movement.Jumped -= PlayJumpAnimation;
        _movement.Attacked -= PlayAttackAnimation;
        _movement.Running -= PlayerRunningAnimation;
    }

    private void PlayJumpAnimation()
    {
        _animator.Play(AnimatorData.Params.Jump);
    }

    private void PlayAttackAnimation()
    {
        _animator.Play(AnimatorData.Params.Attack);
        
    }

    private void PlayerRunningAnimation(bool isRunning)
    {
        _animator.SetBool(AnimatorData.Params.HeroRun, isRunning);
    }
}