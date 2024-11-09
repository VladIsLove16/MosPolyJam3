using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Weapon Weapon;
    public Weapon GetWeapon() { return Weapon; }
}
