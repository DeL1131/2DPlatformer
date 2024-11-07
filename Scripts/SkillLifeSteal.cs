using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Player))]

public class SkillLifeSteal : MonoBehaviour
{
    private float _duration = 6f;
    private float _currentDuration = 0;
    private float _healingAmount = 5;
    private float _damage = 5;
    private float _damageInterval = 1f;
    private float _currentInterval = 0;

    private PlayerInput _playerInput;
    private Player _player;
    private Collider2D _target;
    private List<Collider2D> _findedTargets = new List<Collider2D>();
    private IDamagable _iDamagable;

    public float AbilityRange { get; private set; } = 1.5f;
    public float Cooldown { get; private set; } = 10;
    public bool IsAbilityActive { get; private set; } = false;
    public float CurrentCooldown { get; private set; } = 0;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _playerInput.UseLifeStealAbility += ActivetedAbility;
    }

    private void OnDisable()
    {
        _playerInput.UseLifeStealAbility -= ActivetedAbility;
    }

    private void FixedUpdate()
    {
        _currentInterval += Time.fixedDeltaTime;
        _currentDuration -= Time.fixedDeltaTime;
        CurrentCooldown -= Time.fixedDeltaTime;

        if (IsAbilityActive)
        {
            FindTargets();
        }

        if (IsAbilityActive && _target != null && _iDamagable != null && _currentInterval >= _damageInterval)
        {
            DamagePerInterval();
        }

        if (_currentDuration <= 0)
        {
            DeactivetedAbility();
        }
    }

    private void ActivetedAbility()
    {
        IsAbilityActive = true;
        CurrentCooldown = Cooldown;
        _currentDuration = _duration;
    }

    public void FindTargets()
    {
        _findedTargets.Clear();

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, AbilityRange, _player.LayerMask);

        foreach (Collider2D target in targets)
        {
            if (_player.LayerMask == (_player.LayerMask | (1 << target.gameObject.layer)) && target.gameObject.TryGetComponent(out IDamagable damagable))
            {
                if (!_findedTargets.Contains(target))
                {
                    _findedTargets.Add(target);
                }
            }
        }

        FindClosestTarget(_findedTargets);
    }

    private void FindClosestTarget(List<Collider2D> targets)
    {
        if (targets.Count > 1)
        {
            float currentDistance;
            float closestEnemy = Mathf.Infinity;

            foreach (Collider2D target in targets)
            {
                currentDistance = Vector3Extensions.SqrDistance(transform.position, target.transform.position);

                if (currentDistance < closestEnemy)
                {
                    closestEnemy = currentDistance;
                    _target = target;
                }
            }
        }
        else if (targets.Count == 1)
        {
            _target = targets[0];
        }

        if (targets.Count == 0)
        {
            _target = null;
            _iDamagable = null;
        }
        else if (_target != null && _target.gameObject.TryGetComponent(out IDamagable iDamagable))
        {
            _iDamagable = iDamagable;
        }
    }

    private void DamagePerInterval()
    {
        _currentInterval = 0;
        _iDamagable.TakeDamage(_damage);
        _player.Healing(_healingAmount);
    }

    private void DeactivetedAbility()
    {
        IsAbilityActive = false;
    }
}