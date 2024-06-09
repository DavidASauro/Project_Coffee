using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public AbstractWeapon currentWeapon;
    private bool isReloading = false;
    
    public Transform WeaponPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentWeapon != null && !isReloading)
        {
            currentWeapon.PrimaryAction(WeaponPoint.position, WeaponPoint.rotation);
        }

        if(Input.GetButtonDown("Fire2"))
        {
            
        }

        if(Input.GetKeyDown(KeyCode.R) && !isReloading && currentWeapon is IReloadable)
        {
            isReloading = true;
            StartCoroutine(ReloadRangedWeapon());
            isReloading = false;
        }

        if (!isReloading)
        {
            reloadOnEmpty();
        }
        
    }
    IEnumerator ReloadRangedWeapon()
    {
        if(currentWeapon is RangedWeapon weapon)
        {
            isReloading = true;
            yield return weapon.Reload();
            isReloading = false;
        }
    }

    private void reloadOnEmpty()
    {
        if (currentWeapon is RangedWeapon rangeWeapon)
        {
            if (rangeWeapon.currentAmmoCount <= 0)
            {
                StartCoroutine(ReloadRangedWeapon());
            }
        }
    }

   
}



