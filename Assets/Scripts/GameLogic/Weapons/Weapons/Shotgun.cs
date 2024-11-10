using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    protected new ShotGunInfo weaponInfo;
    public override void Shoot()
    {
        if (isReloading || Time.time - lastFireTime < 1f / weaponInfo.firerate || currentAmmo <= 0)
            return;

        // Update the time of the last shot and reduce ammo
        lastFireTime = Time.time;
        currentAmmo--;

        // Play sound and particle effect
        SoundManager.PlaySound(weaponInfo.sound);
        _particleSystem?.Play();

        // Calculate directions for multiple shots
        Vector2 targetDirection = PointerInput.GetPointerInputVector2();
        Quaternion leftRotation = Quaternion.Euler(0, 0, weaponInfo.spreadAngle);
        Quaternion rightRotation = Quaternion.Euler(0, 0, -weaponInfo.spreadAngle);

        // Emit main shot and two additional shots with angle variations
        emitter.Emmit(targetDirection, weaponInfo);               // Center shot
        emitter.Emmit(leftRotation * targetDirection, weaponInfo); // Left shot
        emitter.Emmit(rightRotation * targetDirection, weaponInfo); // Right shot

        // If the weapon is out of ammo, trigger reload
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }
}
