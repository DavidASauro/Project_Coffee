using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;

    private void Update()
    {
        checkInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;

    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started) { 
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            dashInputStartTime = Time.time;
        }
    }

    public void UseDash() => DashInput = false;

    public void UseJumpInput() => JumpInput = false;

    private void checkInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }

}
