using System;
using UnityEngine;
using UnityEngine.Pool;
[RequireComponent(typeof(Collider2D))]
public class HealthComponent : MonoBehaviour, IDamageable, IStunable
{
    [SerializeField] private float maxHealth = 100;
    public float MaxHealth
    {
        get { return maxHealth; }
        private set { maxHealth = value; }
    }
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float invulnerabilityTime;
    protected float lastTimeDamaged;
    [SerializeField]
    GetHitParticleEffect GetHitParticleEffect;
    public bool dead = false;
    private Animator anim;
    DamageModificationSpawner damageModificationSpawner;
    ItemSpawner itemSpawner;
    [SerializeField]
    SoundManager.Sound GetHitSound;
    public Action died;
    [SerializeField]
    private bool IsPlayer = false;
    [SerializeField]
    private bool IsBoss = false;
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
        else
        {

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
            Debug.Log($"Неуязвимость НЕ активна,  GetHitParticleEffect?.Emit");
            GetHitParticleEffect?.Emit(damageParameters.enemyCollision);
            Debug.Log($"Неуязвимость НЕ активна,  GetHitParticleEffect?.Emit");
        }
    }

    protected bool IsInvulnerable()
    {
        return lastTimeDamaged + invulnerabilityTime > Time.time;
    }

    protected virtual void ApplyDamage(float damage)
    {
        CurrentHealth -= damage;
        SoundManager.PlaySound(GetHitSound,transform.position);
        healthChanged?.Invoke();
        Debug.Log($"Сущность {gameObject.name} получила урон: " + damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        if (GameSettings.EndlessMode)
        {
            damageModificationSpawner.SpawnChooseOne(transform);
        }
        else
        {
            GameObject damageModification = damageModificationSpawner.Spawn(transform, out DamagePickup pickup, new SpawnParametrs() { chance = 30 });
            if (damageModification == null)
            {
                itemSpawner.Spawn(transform, out SpawnObject itemObject, new SpawnParametrs() { chance = 12 });
            }
        }
        if(!IsPlayer && !IsBoss)
        {
            ServiceLocator.Current.Get<ScoreManager>().AddScore((int)maxHealth);
        }
        if(IsBoss)
        {
            ServiceLocator.Current.Get<ScoreManager>().AddScore(1000-(int)Time.time);
            ServiceLocator.Current.Get<EventBus>().Invoke<GameWon>(new GameWon());
            PlayerPrefs.SetInt("gameWon" ,1);
        }
        died?.Invoke();
        Destroy(gameObject);
    }
    public void SetMaxHealth(float value)
    {
        Debug.Log("maxHealth changed");
        maxHealth = value;
    }

    public virtual void ApplyDamage(DamageParameters damageParameters)
    {
        TakeDamage(damageParameters, out bool Damaged);
    }
    public Action OnStun;
    public void Stun(float stunDuration)
    {
        OnStun?. Invoke();
    }
}
