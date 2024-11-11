using UnityEngine;

public class EnemyWeapon : Weapon
{
    [SerializeField]
    Player  Player;
    protected override void SetTarget()
    {
        target = Player.transform.position;
    }
}