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

    public virtual void HandInput()
    {
        Vector2 moveInput = CharactorInputSystem.Instance.PlayerMove;
        InputX = moveInput.x;
        MoveMentStateMachine.reusableData.InputX = InputX;
        // 设置目标水平速度
        MoveMentStateMachine.reusableData.targetHSpeed = InputX * MoveMentStateMachine.reusableData.maxWalkSpeed;

        SetInputXAnimator();
        // 处理跳跃输入缓冲和土狼时间
    }

    public virtual void Update()
    {
        HandleJumpInput();
        MoveToTargetSpeed();
        // 处理角色转向
        HandleCharacterFacing(MoveMentStateMachine.reusableData.currentHSpeed);
    }

    #endregion


    /// <summary>
    /// 处理跳跃输入，包括跳跃缓冲和土狼时间
    /// </summary>
    protected virtual void HandleJumpInput()
    {
        var reusableData = MoveMentStateMachine.reusableData;

        // 更新最后在地面的时间
        if (IsGrounded())
        {
            reusableData.lastGroundedTime = Time.time;
        }

        // 检查跳跃输入
        if (CharactorInputSystem.Instance.JumpWasPressedThisFrame)
        {
            reusableData.lastJumpPressTime = Time.time;
            reusableData.jumpPressTime = 0f; // 重置按键时间
        }

        // 如果跳跃键正在被按下，累计按键时间
        if (CharactorInputSystem.Instance.JumpPressed)
        {
            reusableData.jumpPressTime += Time.deltaTime;
        }

        // 检查是否可以跳跃（跳跃缓冲 + 地形边缘缓冲时间）
        bool canJump = CanJump();
        bool wantsToJump = Time.time - reusableData.lastJumpPressTime <= reusableData.jumpBufferTime;

        if (canJump && wantsToJump && !reusableData.isJumping)
        {
            // 重置跳跃相关时间，防止重复跳跃
            reusableData.lastJumpPressTime = -reusableData.jumpBufferTime;
            MoveMentStateMachine.ChangeState(MoveMentStateMachine.jumpingState);
        }
    }

    /// <summary>
    /// 检查是否可以跳跃（考虑土狼时间）
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanJump()
    {
        var reusableData = MoveMentStateMachine.reusableData;

        // 在地面上或在土狼时间内
        bool isGroundedOrCoyote = IsGrounded() ||
            (Time.time - reusableData.lastGroundedTime <= reusableData.coyoteTime);

        return isGroundedOrCoyote && !reusableData.isJumping;
    }

    /// <summary>
    /// 检查是否在地面上
    /// </summary>
    /// <returns></returns>
    protected virtual bool IsGrounded()
    {
        return MoveMentStateMachine.player.animator.GetBool(AnimatorID.isGrounded);
    }

    protected virtual bool IsFalling()
    {
        return MoveMentStateMachine.player.animator.GetBool(AnimatorID.isFalling);
    }

    protected virtual void SetInputXAnimator()
    {
        MoveMentStateMachine.player.animator.SetBool(AnimatorID.ishaveInputX, MoveMentStateMachine.reusableData.InputX != 0 ? true : false);
    }




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

    #region 动画事件
    public virtual void OnAnimationExitEvent()
    {

    }

    public virtual void OnAnimationTranslateEvent(IState state)
    {
    }

    #endregion




    #region 输入事件

    protected virtual void AddInputActionCallBacks()
    {
        CharactorInputSystem.Instance.inputActions.PlayerInput.Dash.started += OnDashStart;
        CharactorInputSystem.Instance.inputActions.PlayerInput.Jump.started += OnJumpStart;
        CharactorInputSystem.Instance.inputActions.PlayerInput.Jump.canceled += OnJumpCanceled;
        //攻击事件
        CharactorInputSystem.Instance.inputActions.PlayerInput.Attack.started += OnAttackStart;
    }

    

    protected virtual void RemoveInputActionCallBacks()
    {
        CharactorInputSystem.Instance.inputActions.PlayerInput.Dash.started -= OnDashStart;
        CharactorInputSystem.Instance.inputActions.PlayerInput.Jump.started -= OnJumpStart;
        CharactorInputSystem.Instance.inputActions.PlayerInput.Jump.canceled -= OnJumpCanceled;
        CharactorInputSystem.Instance.inputActions.PlayerInput.Attack.started -= OnAttackStart;


    }

    protected virtual void OnDashStart(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    protected virtual void OnJumpStart(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // 跳跃开始处理在HandleJumpInput中进行
    }

    protected virtual void OnJumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // 跳跃取消处理，主要用于跳跃状态中的可变跳跃高度
    }

    protected virtual void OnAttackStart(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
    }

    #endregion
}
