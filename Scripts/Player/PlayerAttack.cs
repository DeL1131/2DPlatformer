using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent (typeof(Player))]

public class PlayerAttack : MonoBehaviour
{
    private InputManager _inputManager;
    private Player _player;

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _player = GetComponent<Player>();
        _inputManager.Mouse0Pressed += Attack;
    }

    private void OnDisable()
    {
        _inputManager.Mouse0Pressed -= Attack;
    }

    private void Attack()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _player.AttackRange, _player.LayerMask);

        if (targets != null)
        {
            foreach (Collider2D target in targets)
            {
                if (target.gameObject.TryGetComponent(out IDamagable damagable))
                {
                    damagable.TakeDamage(_player.Damage);
                    return;
                }
            }
        }
    }
}