using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Weapons/BaseRange")]
public class RangedWeapon : AbstractWeapon, IReloadable
{
    public int totalAmmoCount;
    public float reloadTime;
    public float currentAmmoCount;
    public float projectileSpeed;
    public float projectileMass;
    public float penetrationValue;
    public float fireRate;
    protected bool isReloading;

    public GameObject projectilePrefab;

  

    public override void PrimaryAction(Vector3 startPosition, Quaternion rotation)
    {
        if (isReloading)
        {
            return;
        }
        else
        {
            Instantiate(projectilePrefab, startPosition, rotation);
            currentAmmoCount--;
        }
 
    }

    public virtual IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmoCount = totalAmmoCount;
    }

    public void secondaryAction()
    {

    }
}
