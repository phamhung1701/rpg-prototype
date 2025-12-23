using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetState : PlayerBaseState
{
    private readonly int TargetingHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetForwardHash = Animator.StringToHash("TargetForward");
    private readonly int TargetRightHash = Animator.StringToHash("TargetRight");
    private const float animatorDamping = 0.1f;

    public PlayerTargetState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.Play(TargetingHash); 
        stateMachine.InputReader.TargetEvent += OnCancel;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
        }

        if (stateMachine.targeter.currentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        // Movement
        Move(CalculateMovement() * stateMachine.movementSpeed, deltaTime);
        
        // Rotation
        FaceTarget();

        // Animation
        Animate(deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnCancel;
    }   

    private void OnCancel()
    {
        stateMachine.targeter.Cancel(); 

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = Vector3.zero;

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementInput.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementInput.y;

        return movement;
    }

    private void Animate(float deltaTime)
    {
        if (stateMachine.InputReader.MovementInput.y == 0)
        {
            stateMachine.animator.SetFloat(TargetForwardHash, 0, animatorDamping, deltaTime);
        } 
        else 
        {
            int value = stateMachine.InputReader.MovementInput.y > 0 ? 1 : -1;
            stateMachine.animator.SetFloat(TargetForwardHash, value, animatorDamping, deltaTime);
        }

        if (stateMachine.InputReader.MovementInput.x == 0)
        {
            stateMachine.animator.SetFloat(TargetRightHash, 0, animatorDamping, deltaTime);
        }
        else 
        {
            int value = stateMachine.InputReader.MovementInput.x > 0 ? 1 : -1;
            stateMachine.animator.SetFloat(TargetRightHash, value, animatorDamping, deltaTime);
        }
    }
}
