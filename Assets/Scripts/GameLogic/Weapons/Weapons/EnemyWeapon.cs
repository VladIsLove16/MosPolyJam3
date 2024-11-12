using UnityEngine;

public class EnemyWeapon : Weapon
{
    Player  Player;
    public override void Awake()
    {
        base.Awake();
        Player = ServiceLocator.Current.Get<Player>();
    }
    protected override void SetTarget()
    {
        target = Player.transform.position;
    }
}