using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{ 
    public Transform projectileWeaponPoint;
    public GameObject projectilePrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab,projectileWeaponPoint.position,projectileWeaponPoint.rotation);
    }
}
