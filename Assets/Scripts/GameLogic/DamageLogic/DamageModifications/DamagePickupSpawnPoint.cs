using System;
using UnityEngine;

public class DamagePickupSpawnPoint : MonoBehaviour
{
    private DamagePickup damagePickup;
    public void Occupie(DamagePickup damagePickup)
    {
        this.damagePickup  = damagePickup;
    }
    internal bool IsOccupied()
    {
        bool occupied = damagePickup != null;
        return occupied;
    }
}