using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
[RequireComponent (typeof(SkillLifeSteal))]

public class PlayerInput : MonoBehaviour
{
    private const KeyCode CommandJump = KeyCode.Space;
    private const KeyCode CommandAttack = KeyCode.Mouse0;
    private const KeyCode CommandUseLifeStealAbility = KeyCode.Alpha1;

    private readonly string Horizontal = "Horizontal";

    private Vector3 _direction;
    private Attacker _attacker;
    private SkillLifeSteal _skillLifeSteal;
    private GroundDetectionHandler _groundDetectionHandler;

    public event Action SpacePressed;
    public event Action Mouse0Pressed;
    public event Action<Vector3> OnDirectionInput;
    public event Action UseLifeStealAbility;

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _skillLifeSteal = GetComponent<SkillLifeSteal>();
        _groundDetectionHandler = GetComponent<GroundDetectionHandler>();
    }

    private void Update()
    {
        _direction = new Vector2(Input.GetAxis(Horizontal), 0f);
        OnDirectionInput?.Invoke(_direction);

        if (Input.GetKeyDown(CommandAttack))
        {
            if (_attacker.AttackCooldown <= 0)
                Mouse0Pressed?.Invoke();
        }

        if (Input.GetKeyDown(CommandJump) && _groundDetectionHandler.IsGround)
        {
            SpacePressed?.Invoke();
        }

        if (Input.GetKeyDown(CommandUseLifeStealAbility))
        {
            if(_skillLifeSteal.CurrentCooldown <= 0)
            UseLifeStealAbility?.Invoke();
        }
    }
}