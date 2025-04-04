using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected ParticleSystem _particleSystem;
    [SerializeField] public float rotationSpeed = 1f;
    [SerializeField] float radius; // Distance at which rotation occurs
    [SerializeField] bool lookToTarget;
    [SerializeField] bool rotateToTarget;
    protected Emitter emitter;
    protected Vector2 target;
    [SerializeField] Transform Host;
    [SerializeField]  protected WeaponInfo weaponInfo;
    public Action ammoChanged;

    HealthComponent healthComponent;
    protected float lastFireTime = -Mathf.Infinity;
    protected int currentAmmo;
   
    protected bool isReloading;
    public virtual void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        emitter = GetComponent<Emitter>();
        healthComponent = GetComponentInParent<HealthComponent>();
        healthComponent.OnStun+=OnStun;

    }
    bool stunned = false;
    private float lastTimeStunned;
    private void OnStun()
    {
        stunned = true;
        lastTimeStunned = Time.time;

    }

    public virtual void Start()
    {
        currentAmmo = weaponInfo.bulletsInShop;
    }
    private void Update()
    {
        if (lastTimeStunned + 0.5f < Time.time)
        {
            stunned = false;
        }
        if (!stunned)
        {
            SetTarget();
            Look(target);
            Rotate(target);
        }
    }
    protected virtual void SetTarget()
    {
        target = PointerInput.GetPointerInputVector2();
    }
    public Emitter GetEmmiter()
    {
        if(emitter == null)
            emitter = GetComponent<Emitter>();
        return emitter;
    }
    private void Rotate(Vector2 target)
    {
        if (rotateToTarget)
        {
            if(target.x>transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs( transform.localScale .x)*- 1, Mathf.Abs(transform.localScale.y) * - 1f);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, Mathf.Abs(transform.localScale.y) * 1f);
            }
            Vector2 direction = target - (Vector2)Host.position;
            Vector2 position = direction.normalized * radius;
            transform.localPosition = position;
        }
    }

    private void Look(Vector2 target)
    {
        if (lookToTarget)
        {
            Vector2 from = Host.position;
            Vector2 direction = from - target;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


    public virtual void Shoot()
    {
        Debug.Log("Shoot");
        // Check if the weapon is reloading or if the fire rate limit is active
        if (ReloadingOrShotIntervalOrEmpty())
            return;

        // Update the time of the last shot and reduce ammo
        lastFireTime = Time.time;
        currentAmmo--;
        ammoChanged?.Invoke();
        // Play sound and particle effects
        SoundManager.PlaySound(weaponInfo.sound);
        if (_particleSystem != null)
            _particleSystem.Play();

        // Emit projectiles
        emitter.Emmit(target, weaponInfo);

        // If the weapon is out of ammo, trigger reload
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private bool ReloadingOrShotIntervalOrEmpty()
    {
        return isReloading || Time.time - lastFireTime < weaponInfo.firerate || currentAmmo <= 0;
    }

    protected IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(weaponInfo.reloadTime);
        currentAmmo = weaponInfo.bulletsInShop;  // Refill ammo
        ammoChanged?.Invoke();
        isReloading = false;
    }

    internal string GetDescription()
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (currentAmmo == 0)
        {
            stringBuilder.Append("reloading...");
        }
        else
        {
            stringBuilder.Append(currentAmmo.ToString());
            stringBuilder.Append("/");
            stringBuilder.Append(weaponInfo.bulletsInShop);
        }
        return stringBuilder.ToString();
    }
}
