using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputManager))]

public class Mover : MonoBehaviour
{
    private readonly string Horizontal = "Horizontal";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private InputManager _inputManager;

    public event Action<bool> Running;
    public event Action<Vector3> RotationRequested;

    public Vector3 Direction { get; private set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GetComponent<InputManager>();
        _inputManager.SpacePressed += Jump;
    }

    private void OnDisable()
    {
        _inputManager.SpacePressed -= Jump;
    }

    private void FixedUpdate()
    {
        Direction = new Vector2(Input.GetAxis(Horizontal), 0f);

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
}