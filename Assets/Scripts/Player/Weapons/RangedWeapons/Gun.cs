using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : RangedWeapon
{
    public GameObject projectilePrefab;
    public Transform projectileWeaponPoint;

    public override void shoot()
    {
        base.shoot();
        Instantiate(projectilePrefab, projectileWeaponPoint.position, projectileWeaponPoint.rotation);
    }
}
