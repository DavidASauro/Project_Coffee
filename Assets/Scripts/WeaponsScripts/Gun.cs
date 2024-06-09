using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Weapons/BaseRange/Gun")]
public class Gun : RangedWeapon
{
    public override IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmoCount = totalAmmoCount;
        
    }

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
}
