using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputReader : MonoBehaviour, Controls.IKeyboardActions
{
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public bool isAttacking;
    
    private Controls controls;

    public Vector2 MovementInput { get; private set; }

    void Start()
    {
        controls = new Controls();
        controls.Keyboard.SetCallbacks(this);

        controls.Keyboard.Enable();
    }

    private void OnDestroy()
    {
        controls.Keyboard.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        DodgeEvent.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        TargetEvent.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
        }

        if (context.canceled)
        {
            isAttacking = false;
        }
    }
}
