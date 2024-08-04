using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{
    private const KeyCode Jump = KeyCode.Space;
    private const KeyCode Attack = KeyCode.Mouse0;

    public readonly string Horizontal = "Horizontal";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    public event Action Jumped;
    public event Action Attacked;
    public event Action<bool> Running;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxis(Horizontal), 0f);

        transform.Translate(_speed * Time.deltaTime * direction);

        if (Input.GetKeyDown(Jump))
        {
            Jumped?.Invoke();
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        if (direction.x != 0)
        {
            Running?.Invoke(true);

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            Running?.Invoke(false);
        }

        if (Input.GetKeyDown(Attack))
        {
            Attacked?.Invoke();
        }
    }
}