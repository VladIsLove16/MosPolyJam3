using Unity.VisualScripting;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    DamageApplier damageApplier;
    [SerializeField]
    GameObject bulletpf;
    [SerializeField]
    UnityEngine.Transform spawnPoint;
    UnityEngine.Transform BulletParent;
    private void Awake()
    {
        BulletParent = ServiceLocator.Current.Get<BulletParent>().transform;
    }

    public void Emmit(GameObject pf, Vector2 from, Vector2 target, Quaternion rotation, UnityEngine.Transform parent, float speed, WeaponInfo weaponInfo)
    {
        // Calculate direction based on the weapon's forward vector, always pointing in the desired "forward" direction
        Vector3 direction = (target - (Vector2)from).normalized;

        // If you want the bullet to always move in the direction the weapon is facing (even if the cursor is behind it)
        // You can use transform.forward or spawnPoint.forward for fixed direction based on weapon's rotation.
        direction = -spawnPoint.right.normalized; // Ensure the bullet is always emitted in the weapon's facing direction.

        GameObject bulletGO = Instantiate(pf, from, rotation, parent);
        Debug.Log("Bulltet Instantiated and emmited");
        Bullet bullet = bulletGO.GetOrAddComponent<Bullet>();
        bullet.Init(direction, weaponInfo);

        bulletGO.GetOrAddComponent<BaseDamageDealer>().SetDamageApplier(damageApplier);
    }

    public void Emmit(Vector2 target, WeaponInfo weaponInfo)
    {
        Emmit(bulletpf, spawnPoint.position, target, Quaternion.identity, BulletParent, weaponInfo.bulletSpeed, weaponInfo);
    }

    public void SetDamageApplier(DamageApplier damageApplier)
    {
        this.damageApplier = damageApplier;
    }
}
