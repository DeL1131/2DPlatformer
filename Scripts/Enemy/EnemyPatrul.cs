using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyAggro))]

public class EnemyPatrul : MonoBehaviour, IFlippable
{
    [SerializeField] private Transform[] _allPlacesPoint;
    [SerializeField] private float _speed;

    private Vector3 _target;
    private EnemyAggro _enemyAggro;

    private int _numberPoint;
    private float _arrivalThreshold = 0.3f;

    public Vector3 Direction { get; private set; }
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
            _target = _allPlacesPoint[_numberPoint].transform.position;

            Direction = (_target - transform.position).normalized;

            transform.position = Vector3.MoveTowards(transform.position, _target, Speed * Time.deltaTime);

            if (IsTargetReached())
            {
                NextPoint();
            }
        }
    }

    public void GetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    public bool IsTargetReached()
    {
        return transform.position.IsEnoughClose(_target, _arrivalThreshold);
    }

    private void NextPoint()
    {
        _numberPoint = (++_numberPoint) % _allPlacesPoint.Length;

        _target = _allPlacesPoint[_numberPoint].transform.position;
    }
}