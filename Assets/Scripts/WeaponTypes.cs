using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypes : MonoBehaviour
{


}
public enum weaponType
{
    baseweapon,
    advancedweapon
}
public class weaponStats
{
    public string weaponName;
    public weaponType typeOfWeapon;
    public float damage;
    public float attackpeed;

    

    
}


