using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerShootPistol);
        if (_particleSystem != null)
            _particleSystem?.Play();
        emitter.Emmit(PointerInput.GetPointerInputVector2(),weaponInfo);
    }
}
