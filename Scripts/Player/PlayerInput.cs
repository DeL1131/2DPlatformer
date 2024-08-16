using System;
using UnityEngine;

[RequireComponent(typeof(Attacker))]

public class PlayerInput : MonoBehaviour
{
    private const KeyCode CommandJump = KeyCode.Space;
    private const KeyCode CommandAttack = KeyCode.Mouse0;

    private readonly string Horizontal = "Horizontal";

    private Vector3 _direction;
    private Attacker _attacker;

    public event Action SpacePressed;
    public event Action Mouse0Pressed;
    public event Action<Vector3> OnDirectionInput;

    private void Start()
    {
        _attacker = GetComponent<Attacker>();
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

        if (Input.GetKeyDown(CommandJump))
        {
            SpacePressed?.Invoke();
        }
    }
}