using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _AttackRange;

    public Enemy Target { get; private set; }
    public float Speed { get; private set; }

    private void Awake()
    {
        Speed = _speed;
    }

    public void SetTargetEnemy(Enemy target)
    {
        Target = target;
    }
}
