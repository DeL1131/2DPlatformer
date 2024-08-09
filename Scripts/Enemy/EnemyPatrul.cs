using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyAggro))]

public class EnemyPatrul : MonoBehaviour
{
    [SerializeField] private Transform[] _allPlacesPoint;
    [SerializeField] private float _speed;

    private Transform _target;
    private EnemyAggro _enemyAggro;

    private int _numberPoint;
    private float _arrivalThreshold = 0.3f;

    public float Speed { get; private set; }

    private void Awake()
    {
        Speed = _speed;
        _enemyAggro = GetComponent<EnemyAggro>();
    }

    private void Update()
    {
        if (_enemyAggro.IsHaveAggro == false)
        {
            _target = _allPlacesPoint[_numberPoint];

            Vector3 direction = (_target.position - transform.position).normalized;

            if (direction.x != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            transform.position = Vector3.MoveTowards(transform.position, _target.position, Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _target.position) < _arrivalThreshold)
            {
                NextPoint();
            }
        }
    }

    private void NextPoint()
    {
        _numberPoint = (++_numberPoint) % _allPlacesPoint.Length;

        _target = _allPlacesPoint[_numberPoint].transform;
    }
}