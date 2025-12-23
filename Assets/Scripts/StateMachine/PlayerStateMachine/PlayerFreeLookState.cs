using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookHash = Animator.StringToHash("FreeLookBlendTree");
    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(FreeLookHash, 0.5f);
        stateMachine.InputReader.TargetEvent += OnTarget;
    }

    public override void Tick(float deltaTime)
    {
        // Switch to Attack State 
        if (stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
        }


        // Movement
        Vector2 direction = stateMachine.InputReader.MovementInput;
        Vector3 movement = calculateMovement();

        Move(movement * stateMachine.movementSpeed, deltaTime);

        if (direction == Vector2.zero)
        {
            stateMachine.animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            stateMachine.RotationDamping * deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private Vector3 calculateMovement()
    {
        Vector3 forward = stateMachine.MainCamera.forward;
        Vector3 right = stateMachine.MainCamera.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementInput.y + right * stateMachine.InputReader.MovementInput.x;
    }

    private void OnTarget()
    {
        if (!stateMachine.targeter.SelectTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargetState(stateMachine));
    }
}
