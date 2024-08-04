using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    public float Speed { get; private set; }

    private void Awake()
    {
        Speed = _speed;
    }
}