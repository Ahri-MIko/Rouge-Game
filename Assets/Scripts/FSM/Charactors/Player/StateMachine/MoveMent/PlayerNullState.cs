using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNullState : PlayerMoveMentState
{
    public PlayerNullState(PlayerMoveMentStateMachine mmsm) : base(mmsm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player player = MoveMentStateMachine.player;
        PlayerMoveReusableData moveReusableData = MoveMentStateMachine.reusableData;
        player.rb2D.velocity = new Vector2(0, player.rb2D.velocity.y);
        moveReusableData.targetHSpeed = 0;
        moveReusableData.currentHSpeed = player.rb2D.velocity.x;
    }

    protected override void AddInputActionCallBacks()
    {
        
    }

    protected override void RemoveInputActionCallBacks()
    {
        
    }

    public override void HandInput()
    {

    }
    
    public override void Update()
    {

    }

    public override void Exit()
    {

    }


    public override void OnAnimationTranslateEvent(IState state)
    {
        base.OnAnimationTranslateEvent(state);
        MoveMentStateMachine.ChangeState(state);
    }
}
