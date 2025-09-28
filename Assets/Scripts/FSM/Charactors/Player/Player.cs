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

    [Header("玩家配置")]
    [SerializeField]private PlayerReusableData reusableData = new PlayerReusableData();
    
    public PlayerMoveMentStateMachine movemenStateMachine;
    public PlayerComboStateMachine combomenStateMachine;
    
    // 提供对移动数据的访问
    public PlayerMoveReusableData MoveData => moveData;
    public PlayerComboReusableData ComboData => comboData;

    public PlayerReusableData ReusableData => reusableData;

    protected override void Awake()
    {
        base.Awake();
        movemenStateMachine = new PlayerMoveMentStateMachine(this);
        combomenStateMachine = new PlayerComboStateMachine(this);
    }
    void Start()
    {
        movemenStateMachine.ChangeState(movemenStateMachine.idlingState);
        combomenStateMachine.ChangeState(combomenStateMachine.NullState);
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
                // 切换到攻击状态
                movemenStateMachine.OnAnimationTranslateEvent(movemenStateMachine.moveNullState);
                combomenStateMachine.OnAnimationTranslateEvent(combomenStateMachine.normalAttackState);
                break;
                
            case OnAnimationTranslation.OnEnterAnimationPlayerState.ChargeUp:
                // 切换到蓄力状态
                movemenStateMachine.OnAnimationTranslateEvent(movemenStateMachine.moveNullState);
                combomenStateMachine.OnAnimationTranslateEvent(combomenStateMachine.chargeUpState);
                break;
                
            case OnAnimationTranslation.OnEnterAnimationPlayerState.ChargeAttack:
                // 切换到蓄力攻击状态
                movemenStateMachine.OnAnimationTranslateEvent(movemenStateMachine.moveNullState);
                combomenStateMachine.OnAnimationTranslateEvent(combomenStateMachine.chargeAttackState);
                break;
                
            default:
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

    #region 动画状态检测方法
    
    /// <summary>
    /// 获取当前播放的动画名称
    /// </summary>
    /// <returns>当前动画名称</returns>
    public string GetCurrentAnimationName()
    {
        return AnimationStateChecker.GetCurrentStateName(animator);
    }
    
    /// <summary>
    /// 检查是否正在播放指定动画
    /// </summary>
    /// <param name="animationName">动画名称</param>
    /// <returns>是否正在播放</returns>
    public bool IsPlayingAnimation(string animationName)
    {
        return AnimationStateChecker.IsPlayingAnimation(animator, animationName);
    }
    
    /// <summary>
    /// 获取当前动画播放进度
    /// </summary>
    /// <returns>播放进度(0-1)</returns>
    public float GetAnimationProgress()
    {
        return AnimationStateChecker.GetCurrentAnimationProgress(animator);
    }
    
    /// <summary>
    /// 检查当前动画是否播放完成
    /// </summary>
    /// <returns>是否播放完成</returns>
    public bool IsAnimationComplete()
    {
        return AnimationStateChecker.IsAnimationComplete(animator);
    }
    
    /// <summary>
    /// 获取详细的动画信息（用于调试）
    /// </summary>
    /// <returns>详细动画信息</returns>
    public string GetDetailedAnimationInfo()
    {
        return AnimationStateChecker.GetDetailedAnimationInfo(animator);
    }
    
    #endregion
}
