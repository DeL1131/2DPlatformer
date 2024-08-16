using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(Mover))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Mover _movement;
    private PlayerInput _playerInput;
    private Attacker _attacker;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Mover>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _playerInput.SpacePressed += PlayJumpAnimation;
        _playerInput.Mouse0Pressed += PlayAttackAnimation;
        _movement.Running += PlayerRunningAnimation;
    }

    private void OnDisable()
    {
        _playerInput.SpacePressed -= PlayJumpAnimation;
        _playerInput.Mouse0Pressed -= PlayAttackAnimation;
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