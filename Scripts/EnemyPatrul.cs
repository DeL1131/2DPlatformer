using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyPatrul : MonoBehaviour
{
    [SerializeField] private Transform[] _allPlacesPoint;

    private Transform _target;
    private Enemy _enemy;

    private int _numberPoint;
    private float _arrivalThreshold = 0.3f;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _target = _allPlacesPoint[_numberPoint];
    }

    private void Update()
    {
        Vector3 targetPoint = _target.position;
        Vector3 direction = (targetPoint - transform.position).normalized;

        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _enemy.Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target.position) < _arrivalThreshold)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        _numberPoint++;

        if (_numberPoint == _allPlacesPoint.Length)
            _numberPoint = 0;

        _target = _allPlacesPoint[_numberPoint].transform;
    }
}