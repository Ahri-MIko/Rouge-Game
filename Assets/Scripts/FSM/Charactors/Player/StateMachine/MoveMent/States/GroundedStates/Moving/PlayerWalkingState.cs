using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerMoveMentState
{
    public PlayerWalkingState(PlayerMoveMentStateMachine mmsm) : base(mmsm)
    {

    }

    public override void Enter()
    {
        base.Enter();
        MoveMentStateMachine.player.animator.SetBool(AnimatorID.isWalking, true);
    }

    public override void Update()
    {
        base.Update();

        float currentHSpeed = MoveMentStateMachine.reusableData.currentHSpeed;

        if (IsGrounded() == false && IsFalling())
        {
            MoveMentStateMachine.ChangeState(MoveMentStateMachine.fallingState);
        }
        if (currentHSpeed == 0)
        {
            MoveMentStateMachine.ChangeState(MoveMentStateMachine.idlingState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        MoveMentStateMachine.player.animator.SetBool(AnimatorID.isWalking, false);

    }

    public override void OnAnimationTranslateEvent(IState state)
    {
        MoveMentStateMachine.ChangeState(state);
    }

    protected override void OnDashStart(InputAction.CallbackContext obj)
    {
        MoveMentStateMachine.ChangeState(MoveMentStateMachine.dashState);
    }
}