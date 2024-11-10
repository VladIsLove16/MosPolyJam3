using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "WeaponInfos/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    public Sprite sprite;
    public SoundManager.Sound sound;
    public int baseDamage;
    public float bulletSpeed;
    public float reloadTime;
    public float firerate;
    public int shotBulletAmount;
    public int bulletsInShop;
    public float shootRadius;
    public bool ScreenEdgeDestroy = false;
    public float lifetime;
    public bool LifeTimeDestroy = true;
}
