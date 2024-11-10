using UnityEngine;
using UnityEngine.Pool;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth = 100;
    protected float currentHealth;
    [SerializeField] protected float invulnerabilityTime;
    protected float lastTimeDamaged;
    [SerializeField]
    GetHitParticleEffect GetHitParticleEffect;
    private bool dead = false;
    private Animator anim;
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        GetHitParticleEffect = GetComponent<GetHitParticleEffect>();
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
        currentHealth -= damage;
        Debug.Log($"Сущность {gameObject.name} получила урон: " + damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        //gameObject.SetActive(false);
        dead = true;
    }

    void IDamageable.ApplyDamage(float damage)
    {
        TakeDamage(damage);
    }
}
