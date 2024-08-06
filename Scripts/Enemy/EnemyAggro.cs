using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyAggro : MonoBehaviour
{
    private Enemy _enemy;

    public event Action<Transform> PlayerDetected;
    public event Action<bool> PlayerEnteredAggroZone;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            PlayerEnteredAggroZone?.Invoke(true);
            PlayerDetected?.Invoke(player.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            PlayerEnteredAggroZone?.Invoke(false);
        }
    }
}