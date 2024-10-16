using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public float damage;
    public float fireRate;
    public float range;

    public virtual void shoot()
    {
        //Debug.Log(weaponName + " fired!");
    }

}
