using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent (typeof(EnemyPatrul))]

public class EnemyAggro : MonoBehaviour
{
    private Transform _target;
    private EnemyPatrul _enemyPatrul;

    public  bool IsHaveAggro { get; private set; }

    private void Awake()
    {
        _enemyPatrul = GetComponent<EnemyPatrul>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player _))
        {
            IsHaveAggro = true;
            _target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player _))
        {
            IsHaveAggro = false;
        }
    }

    private void Update()
    {
        if (IsHaveAggro)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            _enemyPatrul.GetDirection(direction);

            transform.position = Vector3.MoveTowards(transform.position, _target.position, _enemyPatrul.Speed * Time.deltaTime);
        }
    }
}