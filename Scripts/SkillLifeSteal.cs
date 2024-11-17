using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Player))]

public class SkillLifeSteal : MonoBehaviour
{
    [SerializeField] private float _healingAmount = 5;
    [SerializeField] private float _damage = 5;

    private float _duration = 6f;
    private float _currentDuration = 0;
    private float _damageInterval = 1f;
    private float _currentInterval = 0;
    private bool _isAbilityActive = false;
    private bool _isCorotineActive = false;

    private PlayerInput _playerInput;
    private Player _player;
    private Collider2D _target;
    private List<Collider2D> _findedTargets = new List<Collider2D>();
    private Coroutine _myCorotine;

    public event Action<bool> ActiveAbilityChange;

    public float AbilityRange { get; private set; } = 1.5f;
    public float Cooldown { get; private set; } = 10;
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
        CurrentCooldown -= Time.fixedDeltaTime;
        _currentDuration -= Time.fixedDeltaTime;

        if (_isAbilityActive)
        {
            FindTargets();
        }

        if (_isAbilityActive && _target != null)
        {
            if (!_isCorotineActive)
            {
                _isCorotineActive = true;
                _myCorotine = StartCoroutine(DamagePerInterval());
            }
        }

        if (_target == null)
        {
            if (_isCorotineActive)
            {
                _isCorotineActive = false;
                StopCoroutine(_myCorotine);
            }
        }

        if (_currentDuration <= 0)
        {
            _isAbilityActive = DeactivetedAbility();

            if (_isCorotineActive && _myCorotine != null)
            {
                StopCoroutine(_myCorotine);
                _isCorotineActive = false;
            }
        }
    }

    private void ActivetedAbility()
    {
        _isAbilityActive = true;
        CurrentCooldown = Cooldown;
        _currentDuration = _duration;

        ActiveAbilityChange?.Invoke(_isAbilityActive);
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
        if (targets.Count == 1)
        {
            _target = targets[0];
            return;
        }

        if (targets.Count == 0)
        {
            _target = null;
            return;
        }

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

    private bool DeactivetedAbility()
    {
        ActiveAbilityChange?.Invoke(_isAbilityActive);
        return false;
    }

    private IEnumerator<WaitForSeconds> DamagePerInterval()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_damageInterval);

        while (_isCorotineActive)
        {
            if (_target.gameObject.TryGetComponent(out IDamagable iDamagable))
            {
                iDamagable.TakeDamage(_damage);
                _player.Healing(_healingAmount);
            }

            yield return waitForSeconds;
        }
    }
}