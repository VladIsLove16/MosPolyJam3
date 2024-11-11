using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 Direction;
    private MoveMechanic moveMechanic;
    private bool destroyBullet = false;
    private Animator anim;
    WeaponInfo weaponInfo;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public Bullet(Vector3 direction)
    {
        this.Direction = direction;
    }

    public void Init(Vector3 direction, WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
        ChangeDirection(direction);
        moveMechanic = new MoveMechanic(direction, weaponInfo.bulletSpeed, gameObject.AddComponent<Rigidbody2D>(), weaponInfo.shootRadius);

        // Destroy after a certain lifetime if specified
        if (weaponInfo.LifeTimeDestroy)
            Destroy(gameObject, weaponInfo.lifetime);
    }

    private void FixedUpdate()
    {
        if (weaponInfo.ScreenEdgeDestroy)
            CheckIfOutOfScreen();

        if (moveMechanic != null && moveMechanic.FixedUpdate())
        {
            // If distance exceeded, mark for destruction
            destroyBullet = true;
        }

        if (destroyBullet)
        {
            moveMechanic = null;

            if (anim != null)
            {
                anim.SetTrigger("impact");
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("bullet impact"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void ChangeDirection(Vector3 vector)
    {
        Direction = vector;
    }


    private void CheckIfOutOfScreen()
    {
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0 || screenPosition.y > 1)
        {
            Destroy(gameObject);
        }
    }

}
