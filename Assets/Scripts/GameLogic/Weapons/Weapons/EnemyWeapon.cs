using UnityEngine;

public class EnemyWeapon : Weapon
{
    Player  Player;
    private void Awake()
    {
        Player = ServiceLocator.Current.Get<Player>();
    }
    protected override void SetTarget()
    {
        target = Player.transform.position;
    }
}