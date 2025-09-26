using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerUpAttack : PlayerAttackStateBase
{
    Vector2 velocity;
    public playerUpAttack(PlayerComboStateMachine comboStateMachine) : base(comboStateMachine)
    {
    }


    public override void Enter()
    {
        base.Enter();
        comboReusableData.hasATKCommand.Value = false;
        velocity = ComboStateMachine.player.rb2D.velocity;
    }

    public override void Update()
    {
        base.Update();
        ComboStateMachine.player.rb2D.velocity = Vector2.zero;
    }

    public override void OnAnimationTranslateEvent(IState state)
    {
        base.OnAnimationTranslateEvent(state);

    }

    public override void OnAnimationExitEvent()
    {
        ComboStateMachine.player.rb2D.velocity = new Vector2(velocity.x,0);
        base.OnAnimationExitEvent();
        //在没有攻击和向上输入的情况下
        if(!(comboReusableData.InputY > 0) && comboReusableData.hasATKCommand.Value == false)
        {
            ComboStateMachine.ChangeState(ComboStateMachine.NullState);
            ComboStateMachine.player.movemenStateMachine.ChangeState(ComboStateMachine.player.movemenStateMachine.fallingState);
        }
        
    }


}
