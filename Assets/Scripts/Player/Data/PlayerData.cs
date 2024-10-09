using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementSpeed = 100f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("Wall Slide State")]
    public float wallSlideSpeed = 3f;

    [Header("Check Variables")]
    public float groundCheckX = 0.5f;
    public float groundCheckY = 0.5f;
    public float groundCastDistance = 0.5f;
    public LayerMask groundMask;
    public float wallCheckDistance = 0.5f;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Abilities")]
    public float dashSpeed = 5f;
    public float dashCoolDown = 0.5f;
    public int amountOfDashes = 1;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Attack State")]
    public GameObject projectilePrefab;
    public float projectileDamage = 1f;
}
