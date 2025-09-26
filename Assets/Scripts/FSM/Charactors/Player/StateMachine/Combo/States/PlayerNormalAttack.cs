using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttack : PlayerAttackStateBase
{
    public PlayerNormalAttack(PlayerComboStateMachine comboStateMachine) : base(comboStateMachine)
    {
    }


    public override void Enter()
    {
        base.Enter();
        comboReusableData.hasATKCommand.Value = false;
    }

    public override void OnAnimationTranslateEvent(IState state)
    {
        base.OnAnimationTranslateEvent(state);
        
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();
       
        if (comboReusableData.hasATKCommand.Value == false)
        {
            ComboStateMachine.player.movemenStateMachine.ChangeState(ComboStateMachine.player.movemenStateMachine.idlingState);
            ComboStateMachine.ChangeState(ComboStateMachine.NullState);
        }
    }

   
}
