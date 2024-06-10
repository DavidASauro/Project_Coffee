using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractWeapon : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float range;
    public Sprite weaponSprite;

    public abstract void PrimaryAction(Vector3 startPosition, Quaternion rotation);

    public virtual void DisplayStats()
    {
        Debug.Log($"{weaponName} - Damage: {damage}, Range: {range}");
        
    }
}
