using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        Vector3 move = new Vector3(stateMachine.InputReader.MovementInput.x, 0, stateMachine.InputReader.MovementInput.y);
        stateMachine.transform.Translate(move * Time.deltaTime);
    }

    public override void Exit()
    {
        ;
    }
}
