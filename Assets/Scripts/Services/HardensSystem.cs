/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;

public class HardensSystem : MonoBehaviour, IService
{
    public float PlayerDamageMultiplier = 3;
    Player player;

    private EnemySpawner EnemySpawner;
    private void Awake()
    {
        EnemySpawner = ServiceLocator.Current.Get<EnemySpawner>();
        player = ServiceLocator.Current.Get<Player>();
        InvokeRepeating(nameof(RaiseHardens),15f, 15f);
    }
    public void RaiseHardens()
    {
        float currentHealth = EnemySpawner.GetHealth();
        float totalHealth = PlayerDamageMultiplier * player.GetDamageApplier().CalculateDamage() + currentHealth;
        EnemySpawner.IncreaseHealth(totalHealth);
    }
}