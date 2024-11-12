using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMechanic : MonoBehaviour
{
    [SerializeField ]
    List<Collider2D> colliders;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (colliders.Contains(collision.collider))
            return;

    }
}
