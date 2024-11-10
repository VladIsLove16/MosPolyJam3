using System;
using System.Collections;
using System.Runtime.CompilerServices;
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
    [SerializeField]
    Transform Host;
    [SerializeField]
    protected WeaponInfo weaponInfo;


    protected float lastFireTime = -Mathf.Infinity;
    protected int currentAmmo;
    protected bool isReloading;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        emitter = GetComponent<Emitter>();
    }

    private void Update()
    {
        SetTarget();
        Look(target);
        Rotate(target);
    }
    private void SetTarget()
    {
        target = PointerInput.GetPointerInputVector2();
    }
    public Emitter GetEmmiter()
    {
        return emitter;
    }
    private void Rotate(Vector2 target)
    {
        if (rotateToTarget)
        {
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

    public virtual void Start()
    {
        currentAmmo = weaponInfo.bulletsInShop;
    }

    public virtual void Shoot()
    {
        // Check if the weapon is reloading or if the fire rate limit is active
        if (isReloading || Time.time - lastFireTime < weaponInfo.firerate || currentAmmo <= 0)
            return;

        // Update the time of the last shot and reduce ammo
        lastFireTime = Time.time;
        currentAmmo--;

        // Play sound and particle effects
        SoundManager.PlaySound(weaponInfo.sound);
        if(_particleSystem!=null)
            _particleSystem.Play();

        // Emit projectiles
        emitter.Emmit(target, weaponInfo);

        // If the weapon is out of ammo, trigger reload
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    protected IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(weaponInfo.reloadTime);
        currentAmmo = weaponInfo.bulletsInShop;  // Refill ammo
        isReloading = false;
    }
}
