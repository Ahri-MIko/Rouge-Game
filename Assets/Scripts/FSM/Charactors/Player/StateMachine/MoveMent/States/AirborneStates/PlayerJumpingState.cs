using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpingState : PlayerMoveMentState
{
    private bool jumpInputReleased = false;
    private float jumpStartTime;
    
    public PlayerJumpingState(PlayerMoveMentStateMachine mmsm) : base(mmsm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        jumpInputReleased = false;
        jumpStartTime = Time.time;
        
        // 执行跳跃
        PerformJump();
        
        // 设置动画
        MoveMentStateMachine.player.animator.SetBool(AnimatorID.isJumping, true);
        MoveMentStateMachine.reusableData.isJumping = true;
        
        Debug.Log("开始跳跃");
    }

    public override void Update()
    {
        base.Update();
        
        // 检查是否落地
        if (MoveMentStateMachine.player.rb2D.velocity.y <= 0 && IsGrounded() && MoveMentStateMachine.reusableData.InputX == 0)
        {
            MoveMentStateMachine.ChangeState(MoveMentStateMachine.idlingState);
            return;
        }
        else if(MoveMentStateMachine.player.rb2D.velocity.y <= 0 && IsGrounded() && MoveMentStateMachine.reusableData.InputX != 0)
        {
            MoveMentStateMachine.ChangeState(MoveMentStateMachine.walkingState);
            return;

        }

        // 处理可变跳跃高度（如果跳跃键被释放且向上速度大于0，减少向上速度）
        if (jumpInputReleased && MoveMentStateMachine.player.rb2D.velocity.y > 0)
        {
            Vector2 velocity = MoveMentStateMachine.player.rb2D.velocity;
            velocity.y *= 0.5f; // 减少向上速度，实现短跳
            MoveMentStateMachine.player.rb2D.velocity = velocity;
            jumpInputReleased = false; // 只执行一次
        }
    }

    public override void Exit()
    {
        base.Exit();
        MoveMentStateMachine.player.animator.SetBool(AnimatorID.isJumping, false);
        MoveMentStateMachine.reusableData.isJumping = false;
        Debug.Log("退出跳跃状态");
    }

    protected override void AddInputActionCallBacks()
    {
        base.AddInputActionCallBacks();
        CharactorInputSystem.Instance.inputActions.PlayerInput.Jump.canceled += OnJumpReleased;
    }

    protected override void RemoveInputActionCallBacks()
    {
        base.RemoveInputActionCallBacks();
        CharactorInputSystem.Instance.inputActions.PlayerInput.Jump.canceled -= OnJumpReleased;
    }

    private void OnJumpReleased(InputAction.CallbackContext context)
    {
        jumpInputReleased = true;
        Debug.Log("跳跃键释放");
    }

    private void PerformJump()
    {
        var reusableData = MoveMentStateMachine.reusableData;
        var rb2D = MoveMentStateMachine.player.rb2D;
        
        // 根据按键时间决定跳跃力度
        float jumpForce;
        float pressTime = reusableData.jumpPressTime;
        
        if (pressTime >= reusableData.longPressThreshold)
        {
            jumpForce = reusableData.longJumpForce;
            Debug.Log($"长按跳跃，按键时间: {pressTime:F2}s，跳跃力度: {jumpForce}");
        }
        else
        {
            jumpForce = reusableData.shortJumpForce;
            Debug.Log($"短按跳跃，按键时间: {pressTime:F2}s，跳跃力度: {jumpForce}");
        }
        
        // 重置垂直速度并施加跳跃力
        Vector2 velocity = rb2D.velocity;
        velocity.y = jumpForce;
        rb2D.velocity = velocity;
    }


}