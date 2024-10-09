using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public int ammoCapacity;
    public float reloadTime;
    public float projectileSpeed;
   
    public override void shoot()
    {
        base.shoot();
        
    }
}
