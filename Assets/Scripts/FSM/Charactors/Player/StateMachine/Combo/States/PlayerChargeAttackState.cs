using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeAttackState : PlayerAttackStateBase
{
    public PlayerChargeAttackState(PlayerComboStateMachine comboStateMachine) : base(comboStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("进入蓄力攻击状态");
        
        // 重置攻击命令标识
        comboReusableData.hasATKCommand.Value = false;
        
        // 播放蓄力攻击动画
        anim.SetTrigger(AnimatorID.ChargeAttack);
        
        // 重置蓄力状态
        comboReusableData.isCharging.Value = false;
        comboReusableData.isChargeComplete.Value = false;
        comboReusableData.chargeTime = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnAnimationTranslateEvent(IState state)
    {
        base.OnAnimationTranslateEvent(state);
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();
        anim.SetBool(AnimatorID.ChargeComplete, false);
        anim.ResetTrigger(AnimatorID.ChargeAttack);
        // 蓄力攻击动画结束后的处理，和普通攻击类似
        if (comboReusableData.hasATKCommand.Value == false)
        {
            // 没有新的攻击命令，回到移动状态
            ComboStateMachine.player.movemenStateMachine.ChangeState(ComboStateMachine.player.movemenStateMachine.idlingState);
            ComboStateMachine.ChangeState(ComboStateMachine.NullState);
        }

        
        // 如果有新的攻击命令，会通过动画事件或其他方式处理连击
    }

}