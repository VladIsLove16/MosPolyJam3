using System;
using UnityEngine;
using UnityEngine.Pool;
[RequireComponent(typeof(Collider2D))]
public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float invulnerabilityTime;
    protected float lastTimeDamaged;
    [SerializeField]
    GetHitParticleEffect GetHitParticleEffect;
    public bool dead = false;
    private Animator anim;
    DamageModificationSpawner damageModificationSpawner;
    ItemSpawner itemSpawner;
    public Action healthChanged;
        public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            healthChanged?.Invoke();
        }
    }
    protected virtual void Start()
    {
        CurrentHealth = maxHealth;
        anim = GetComponent<Animator>();
        GetHitParticleEffect = GetComponent<GetHitParticleEffect>();
        damageModificationSpawner = ServiceLocator.Current.Get<DamageModificationSpawner>();
        itemSpawner = ServiceLocator.Current.Get<ItemSpawner>();
    }
    void FixedUpdate()
    {
        if (dead)
        {
            anim.SetTrigger("dead");
            if (anim.GetAnimatorTransitionInfo(0).IsName("end anim"))
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(float damage)
    {
        TakeDamage( damage, out bool damaged);
    }

    public void TakeDamage(float damage, out bool damaged)
    {
        if (IsInvulnerable())
        {
            Debug.Log($"Неуязвимость активна, урон {damage} не нанесен.");
            damaged = false;
            return;
        }

        ApplyDamage(damage);
        lastTimeDamaged = Time.time;
        damaged = true;
    }
    public void TakeDamage(DamageParameters damageParameters, out bool damaged)
    {
        TakeDamage(damageParameters.damage, out damaged);
        if (damaged)
        {
            GetHitParticleEffect?.Emit(damageParameters.enemyCollision);
        }
    }

    protected bool IsInvulnerable()
    {
        return lastTimeDamaged + invulnerabilityTime > Time.time;
    }

    protected virtual void ApplyDamage(float damage)
    {
        CurrentHealth -= damage;
        healthChanged?.Invoke();
        Debug.Log($"Сущность {gameObject.name} получила урон: " + damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        GameObject gameObject = damageModificationSpawner.Spawn(transform,out DamagePickup pickup, new SpawnParametrs() { chance = 30});
        if(gameObject==null)
        {
            itemSpawner.Spawn(transform, out ISpawnObject itemObject, new SpawnParametrs() { chance = 12 });
        }
        gameObject.SetActive(false);
    }

    void IDamageable.ApplyDamage(float damage)
    {
        TakeDamage((float)damage);
    }
}
