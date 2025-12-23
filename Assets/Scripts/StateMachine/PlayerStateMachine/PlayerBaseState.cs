using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.controller.Move((motion + stateMachine.forceReceiver.Movement) * deltaTime);
    }

    protected void FaceTarget()
    {
        if(stateMachine.targeter.currentTarget == null) { return; }

        Vector3 direction = stateMachine.targeter.currentTarget.transform.position - stateMachine.transform.position;
        direction.y = 0f;

        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(direction), stateMachine.RotationDamping * Time.deltaTime);
    }
}
