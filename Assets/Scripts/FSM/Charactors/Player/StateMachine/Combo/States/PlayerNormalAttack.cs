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
        Debug.Log("进入普通攻击状态");
        
        comboReusableData.hasATKCommand.Value = false;
        
        // 重置蓄力相关状态
        comboReusableData.isCharging.Value = false;
        comboReusableData.isChargeComplete.Value = false;
        comboReusableData.chargeTime = 0f;
        
        // 播放普通攻击动画
        anim.SetTrigger(AnimatorID.Attack);
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
