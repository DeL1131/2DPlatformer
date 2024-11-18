using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(Mover))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(GroundDetectionHandler))]


public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Mover _movement;
    private PlayerInput _playerInput;
    private GroundDetectionHandler _groundDetectionHandler;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Mover>();
        _playerInput = GetComponent<PlayerInput>();
        _groundDetectionHandler = GetComponent<GroundDetectionHandler>();
    }

    private void OnEnable()
    {
        _playerInput.SpacePressed += PlayJumpAnimation;
        _playerInput.Mouse0Pressed += PlayAttackAnimation;
        _movement.Running += PlayerRunningAnimation;
        _groundDetectionHandler.GroundedChanged += PlaytFlyAnimation;
    }

    private void OnDisable()
    {
        _playerInput.SpacePressed -= PlayJumpAnimation;
        _playerInput.Mouse0Pressed -= PlayAttackAnimation;
        _movement.Running -= PlayerRunningAnimation;
        _groundDetectionHandler.GroundedChanged -= PlaytFlyAnimation;

    }

    private void PlayJumpAnimation()
    {
        _animator.Play(AnimatorData.Params.HeroJump);
    }

    private void PlayAttackAnimation()
    {        
        _animator.Play(AnimatorData.Params.HeroAttack);       
    }

    private void PlayerRunningAnimation(bool isRunning)
    {
        _animator.SetBool(AnimatorData.Params.HeroRun, isRunning);
    }

    private void PlaytFlyAnimation(bool isFly)
    {
        _animator.SetBool(AnimatorData.Params.HeroFly, !isFly);
    }
}