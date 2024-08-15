using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyAggro))]

public class EnemyPatrul : MonoBehaviour
{
    [SerializeField] private Transform[] _allPlacesPoint;
    [SerializeField] private float _speed;

    private Vector3 _target;
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
            _target = _allPlacesPoint[_numberPoint].transform.position;

            Vector3 direction = (_target - transform.position).normalized;

            if (direction.x != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            transform.position = Vector3.MoveTowards(transform.position, _target, Speed * Time.deltaTime);

            if (IsTargetReached())
            {
                NextPoint();
            }
        }
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