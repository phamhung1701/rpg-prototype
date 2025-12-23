using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private bool ForceApplied;
    private float previousFrameTime;
    private Attack currentAttack;
    public PlayerAttackState(PlayerStateMachine stateMachine, int AttackIndex) : base(stateMachine)
    {
        currentAttack = stateMachine.Attacks[AttackIndex];
    }

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(currentAttack.AnimationName, currentAttack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        FaceTarget();
        Move(deltaTime);

        float normalizedTime = GetNormalized();
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            stateMachine.Damage.SetDamage(currentAttack.Damage);

            if (normalizedTime >= currentAttack.ForceTime) 
            {
                TryApplyForce();
            }

            if (stateMachine.InputReader.isAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.targeter.currentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
    }

    public override void Exit()
    {
        
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (currentAttack.ComboStateIndex == -1) { return; }
        if (normalizedTime < currentAttack.AttackCombo) { return; }

        stateMachine.SwitchState(new PlayerAttackState(stateMachine, currentAttack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if (ForceApplied) { return; }
        stateMachine.forceReceiver.AddForce(currentAttack.Force * stateMachine.transform.forward);
        ForceApplied = true;
    }

    private float GetNormalized()
    {
        AnimatorStateInfo currentStateInfo = stateMachine.animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextStateInfo = stateMachine.animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.animator.IsInTransition(0) && nextStateInfo.IsTag("Attack"))
        {
            return nextStateInfo.normalizedTime;
        }
        else if (!stateMachine.animator.IsInTransition(0) && currentStateInfo.IsTag("Attack"))
        {
            return currentStateInfo.normalizedTime;
        } 
        else
        {
            return 0f;
        }
    }
}
