using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletDebug : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Assert(false,collision.gameObject.name);
    }
}
