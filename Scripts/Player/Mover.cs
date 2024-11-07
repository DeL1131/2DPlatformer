using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]

public class Mover : MonoBehaviour, IFlippable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private PlayerInput _inputManager;

    public event Action<bool> Running;

    public Vector3 Direction { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponent<PlayerInput>();

        _inputManager.SpacePressed += Jump;
        _inputManager.OnDirectionInput += GetDirection;
    }

    private void OnDisable()
    {
        _inputManager.SpacePressed -= Jump;
        _inputManager.OnDirectionInput -= GetDirection;
    }

    private void FixedUpdate()
    {
        transform.Translate(_speed * Time.deltaTime * Direction);

        if (Direction.x != 0)
        {
            Running?.Invoke(true);
        }
        else
        {
            Running?.Invoke(false);
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void GetDirection(Vector3 direction)
    {
        Direction = direction;
    }
}