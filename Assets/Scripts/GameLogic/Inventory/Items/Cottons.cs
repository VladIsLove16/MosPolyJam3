using System;
using UnityEngine;

public class Cottons : InventoryItem
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            
        }
    }
    public override void Use()
    {
        base.Use();
        //gameObject.SetActive(true);
    }
}
