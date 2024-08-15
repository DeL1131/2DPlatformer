using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(Mover))]
[RequireComponent(typeof(InputManager))]


public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Mover _movement;
    private InputManager _inputManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Mover>();
        _inputManager = GetComponent<InputManager>();
    }

    private void OnEnable()
    {
        _inputManager.SpacePressed += PlayJumpAnimation;
        _inputManager.Mouse0Pressed += PlayAttackAnimation;
        _movement.Running += PlayerRunningAnimation;
    }

    private void OnDisable()
    {
        _inputManager.SpacePressed -= PlayJumpAnimation;
        _inputManager.Mouse0Pressed -= PlayAttackAnimation;
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