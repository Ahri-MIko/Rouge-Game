using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OnAnimationTranslation;

public class Player : CharacterControllerBase
{
    [Header("移动配置")]
    [SerializeField] private PlayerMoveReusableData moveData = new PlayerMoveReusableData();

    [Header("攻击配置")]
    [SerializeField] private PlayerComboReusableData comboData = new PlayerComboReusableData();
    
    public PlayerMoveMentStateMachine movemenStateMachine;
    public PlayerComboStateMachine combomenStateMachine;
    
    // 提供对移动数据的访问
    public PlayerMoveReusableData MoveData => moveData;
    public PlayerComboReusableData ComboData => comboData;

    protected override void Awake()
    {
        base.Awake();
        movemenStateMachine = new PlayerMoveMentStateMachine(this);
        combomenStateMachine = new PlayerComboStateMachine(this);
    }
    void Start()
    {
        movemenStateMachine.ChangeState(movemenStateMachine.idlingState);
    }

    // Update is called once per frame
    protected override void  Update()
    {
        base.Update();
        movemenStateMachine.HandInput();
        movemenStateMachine.Update();
        combomenStateMachine.HandInput();
        combomenStateMachine.Update();
        
    }


    #region 动画事件处理
    public void OnAnimationTranslateEvent(OnAnimationTranslation.OnEnterAnimationPlayerState playerState)
    {
        switch(playerState)
        {
            case OnAnimationTranslation.OnEnterAnimationPlayerState.Idle:
                // 切换到空闲状态
                movemenStateMachine.OnAnimationTranslateEvent(movemenStateMachine.idlingState);
                break;
                
            case OnAnimationTranslation.OnEnterAnimationPlayerState.Walk:
                // 切换到行走状态
                movemenStateMachine.OnAnimationTranslateEvent(movemenStateMachine.walkingState);
                break;
                
            case OnAnimationTranslation.OnEnterAnimationPlayerState.Dash:
                // 切换到冲刺状态
                movemenStateMachine.OnAnimationTranslateEvent(movemenStateMachine.dashState);
                break;
                
            case OnAnimationTranslation.OnEnterAnimationPlayerState.Jump:
                // 切换到跳跃状态
                movemenStateMachine.OnAnimationTranslateEvent(movemenStateMachine.jumpingState);
                break;
            case OnAnimationTranslation.OnEnterAnimationPlayerState.ATK:
                // 切换到跳跃状态
                movemenStateMachine.OnAnimationTranslateEvent(combomenStateMachine.normalAttackState);
                break;
            default:
                Debug.LogWarning($"未处理的动画状态: {playerState}");
                break;
        }
    }

    public void OnAnimationExitEvent()
    {
        // 通知当前状态机状态处理动画退出事件
        movemenStateMachine.OnAnimationExitEvent();

        combomenStateMachine.OnAnimationExitEvent();
    }

    #endregion
}
