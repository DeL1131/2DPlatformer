using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]
[RequireComponent (typeof(Attacker))]

public abstract class EnemyAnimator : MonoBehaviour
{
    protected Animator Animator;
    protected Enemy Enemy;
    protected Attacker Attacker;

    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        Animator = GetComponent<Animator>();
        Attacker = GetComponent<Attacker>();

        Enemy.Died += PlayEnemyDeathAnimation;
        Attacker.Attacked += PlayEnemyAttackAnimation;
    }

    private void OnDestroy()
    {
        Enemy.Died -= PlayEnemyDeathAnimation;
        Attacker.Attacked -= PlayEnemyAttackAnimation;
    }

    protected abstract void PlayEnemyDeathAnimation();

    protected abstract void PlayEnemyAttackAnimation();
}