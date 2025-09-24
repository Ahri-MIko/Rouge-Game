using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMentState : IState
{
    public PlayerMoveMentStateMachine MoveMentStateMachine;

    float InputX;

    public PlayerMoveMentState(PlayerMoveMentStateMachine mmsm)
    {
        MoveMentStateMachine = mmsm;
    }

    #region IState Interface
    public virtual void Enter()
    {
        Debug.Log("进入状态" + GetType().Name);
        AddInputActionCallBacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionCallBacks();
    }

    public void HandInput()
    {
        Vector2 moveInput = CharactorInputSystem.Instance.PlayerMove;
        InputX = moveInput.x;
        MoveMentStateMachine.reusableData.InputX = InputX;
        // 设置目标水平速度
        MoveMentStateMachine.reusableData.targetHSpeed = InputX * MoveMentStateMachine.reusableData.maxWalkSpeed;
    }

    public virtual void OnAnimationExitEvent()
    {
        
    }

    public virtual void OnAnimationTranslateEvent(IState state)
    {
     
    }

    public virtual void Update()
    {
        MoveToTargetSpeed();
        // 处理角色转向
        HandleCharacterFacing(MoveMentStateMachine.reusableData.currentHSpeed);
    }

    #endregion


    /// <summary>
    /// 玩家水平移动
    /// </summary>
    public virtual void MoveToTargetSpeed()
    {
        var reusableData = MoveMentStateMachine.reusableData;
        var player = MoveMentStateMachine.player;
        
        // 获取当前水平速度
        float currentVelocityX = player.rb2D.velocity.x;
        
        // 计算速度差
        float speedDifference = reusableData.targetHSpeed - currentVelocityX;
        
        // 选择加速度或减速度
        float accelerationRate = Mathf.Abs(reusableData.targetHSpeed) > 0.01f ? 
            reusableData.acceleration : reusableData.deceleration;
        
        // 计算新的水平速度
        float newVelocityX;
        if (Mathf.Abs(speedDifference) <= accelerationRate * Time.deltaTime)
        {
            // 如果速度差很小，直接设置为目标速度
            newVelocityX = reusableData.targetHSpeed;
        }
        else
        {
            // 平滑过渡到目标速度
            newVelocityX = currentVelocityX + Mathf.Sign(speedDifference) * accelerationRate * Time.deltaTime;
        }
        
        // 应用新的速度
        Vector2 newVelocity = new Vector2(newVelocityX, player.rb2D.velocity.y);
        player.rb2D.velocity = newVelocity;
        
        // 更新当前速度记录
        reusableData.currentHSpeed = newVelocityX;
        
    }
    
    /// <summary>
    /// 处理角色面向方向
    /// </summary>
    /// <param name="velocityX">水平速度</param>
    protected virtual void HandleCharacterFacing(float velocityX)
    {
        var player = MoveMentStateMachine.player;
        var reusableData = MoveMentStateMachine.reusableData;
        
        // 只在有明显移动时才改变朝向，避免抖动
        if (Mathf.Abs(velocityX) > reusableData.facingThreshold)
        {
            bool shouldFaceRight = velocityX > 0;
            
            // 只在方向真正改变时才执行翻转
            if (reusableData.facingRight != shouldFaceRight)
            {
                reusableData.facingRight = shouldFaceRight;
                
                // 方案1：使用Scale翻转
                if (reusableData.useScaleFlip)
                {
                    if (shouldFaceRight)
                        player.transform.localScale = new Vector3(-1, 1, 1); // 面向右
                    else
                        player.transform.localScale = new Vector3(1, 1, 1);  // 面向左
                }
            }
        }
    }




    #region 输入事件

    protected virtual void AddInputActionCallBacks()
    {
        CharactorInputSystem.Instance.inputActions.PlayerInput.Dash.started += OnDashStart;
    }

    protected virtual void RemoveInputActionCallBacks()
    {
        CharactorInputSystem.Instance.inputActions.PlayerInput.Dash.started -= OnDashStart;

    }


    protected virtual void OnDashStart(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
    }

    #endregion
}
