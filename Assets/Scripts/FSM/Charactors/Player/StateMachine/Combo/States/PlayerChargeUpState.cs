using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeUpState : PlayerAttackStateBase
{
    public PlayerChargeUpState(PlayerComboStateMachine comboStateMachine) : base(comboStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("进入蓄力状态");
        //当前仅仅作为一个标志,充当blackboard,所有拥有Animator的obj都可从animator中获取信息
        anim.SetBool(AnimatorID.IsCharging, true);
    }


    public override void Exit()
    {
        base.Exit();
        anim.SetBool(AnimatorID.IsCharging, false);
        comboReusableData.isChargeComplete.Value = false;
    }

    public override void Update()
    {
        base.Update();
        
        if(comboReusableData.isChargeComplete.Value)
        {
            anim.SetBool(AnimatorID.ChargeComplete,true);
            ComboStateMachine.ChangeState(ComboStateMachine.chargeAttackState);
        }
    }

    public override void OnAnimationTranslateEvent(IState state)
    {
       base.OnAnimationTranslateEvent(state);
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();
    }

}