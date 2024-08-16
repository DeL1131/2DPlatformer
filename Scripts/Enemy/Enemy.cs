using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent (typeof(Attacker))]

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected LayerMask LayerMask;

    protected Health Health;

    private Attacker _attacker;
    private float _deathDelay = 0.3f;    

    public event Action Died;

    public float CurrentDamage { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();

        CurrentDamage = Damage;
        Health.HealhChanged += VerifyHealth;
    }

    private void OnDisable()
    {
        Health.HealhChanged -= VerifyHealth;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamagable damageble) && collision.gameObject.TryGetComponent<Player>(out Player _))
        {
            _attacker.Attack(LayerMask, AttackRange, Damage);
        }
    }

    private void VerifyHealth(float currentHealth)
    {
        if (Health.CurrentHealth <= 0)
        {
            Died?.Invoke();
            StartCoroutine(StartEnemyDeath());
        }
    }

    private IEnumerator StartEnemyDeath()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_deathDelay);

        yield return waitForSeconds;
        Destroy(gameObject);
    }
}