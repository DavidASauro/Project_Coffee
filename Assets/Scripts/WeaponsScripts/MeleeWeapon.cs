using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapons/BaseMelee")]
public class MeleeWeapon : AbstractWeapon
{
    public float attackSpeed;
    public float knockBack;
    public float stunDuration;
    public float bleedRate;
    public float enemySlowdown;
    public float weaponMass;

    public override void PrimaryAction(Vector3 startPosition, Quaternion rotation)
    {
        
    }
}

    
