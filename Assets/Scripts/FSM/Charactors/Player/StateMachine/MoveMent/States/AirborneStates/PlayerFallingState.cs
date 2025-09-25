using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerMoveMentState
{
    public PlayerFallingState(PlayerMoveMentStateMachine mmsm) : base(mmsm)
    {
    }


    public override void Enter()
    {
        base.Enter();

        // …Ë÷√∂Øª≠
        MoveMentStateMachine.player.animator.SetBool(AnimatorID.isJumping, true);
        MoveMentStateMachine.reusableData.isJumping = true;
    }

    public override void Update()
    {
        base.Update();
        if (MoveMentStateMachine.reusableData.InputX != 0 && IsGrounded() == true)
        {
            MoveMentStateMachine.ChangeState(MoveMentStateMachine.walkingState);
        }
        else if (MoveMentStateMachine.reusableData.InputX == 0 && IsGrounded() == true)
        {
            MoveMentStateMachine.ChangeState(MoveMentStateMachine.idlingState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        MoveMentStateMachine.player.animator.SetBool(AnimatorID.isJumping, false);
        MoveMentStateMachine.reusableData.isJumping = false;
    }




}
