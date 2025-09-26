using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleingState : PlayerMoveMentState
{
    public PlayerIdleingState(PlayerMoveMentStateMachine mmsm) : base(mmsm)
    {
    }

    public override void Enter()
    {
        base.Enter();


    }
    public override void Update()
    {
        base.Update();

        if (MoveMentStateMachine.reusableData.InputX != 0)
        {
            MoveMentStateMachine.ChangeState(MoveMentStateMachine.walkingState);
        }
    }

    protected override void OnDashStart(InputAction.CallbackContext obj)
    {
        MoveMentStateMachine.ChangeState(MoveMentStateMachine.dashState);
    }
}