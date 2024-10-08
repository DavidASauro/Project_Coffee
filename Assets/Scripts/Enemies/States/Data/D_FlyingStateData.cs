using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFlyingStateData", menuName = "Data/State Data/Flying State")]
public class D_FlyingStateData : ScriptableObject
{
    public float flyingSpeed = 1.0f;
    public float tolerance;
    public Vector3 pointA;
    public Vector3 pointB;
 
}
