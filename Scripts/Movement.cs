using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{
    public readonly string Horizontal = "Horizontal";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxis(Horizontal), 0f);

        transform.Translate(_speed * Time.deltaTime * direction);

        if (direction.x != 0)
        {
            _animator.SetBool("isRunning", true);

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
          _animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f); // —брасываем вертикальную скорость перед прыжком
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _animator.Play("Jump");
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _animator.Play("Attack");
        }
    }
}