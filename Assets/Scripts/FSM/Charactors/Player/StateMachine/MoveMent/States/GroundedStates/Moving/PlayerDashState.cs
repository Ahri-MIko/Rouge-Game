using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerMoveMentState
{
    public PlayerDashState(PlayerMoveMentStateMachine mmsm) : base(mmsm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        MoveMentStateMachine.player.animator.SetTrigger(AnimatorID.isDash);
        SetDashVelocity();
    }

    protected override void HandleJumpInput()
    {
        //冲刺不允许跳跃
    }


    public override void OnAnimationTranslateEvent(IState state)
    {
        base.OnAnimationTranslateEvent(state);
    }

    public override void OnAnimationExitEvent()
    {
        base.OnAnimationExitEvent();
        
        float inputX = MoveMentStateMachine.reusableData.InputX;
        
        // 根据玩家当前输入决定下一个状态
        if (Mathf.Abs(inputX) > 0.01f)
        {
            // 玩家仍在输入移动，转入行走状态
            // 重要：立即将速度重置为合理的行走速度，避免从冲刺速度缓慢减速
            var reusableData = MoveMentStateMachine.reusableData;
            float walkSpeed = inputX * reusableData.maxWalkSpeed;
            
            // 立即设置为行走速度，而不是让MoveToTargetSpeed慢慢减速
            MoveMentStateMachine.player.rb2D.velocity = new Vector2(walkSpeed, MoveMentStateMachine.player.rb2D.velocity.y);
            reusableData.currentHSpeed = walkSpeed;
            reusableData.targetHSpeed = walkSpeed;

            if (IsGrounded() == false && IsFalling() == true)
                MoveMentStateMachine.ChangeState(MoveMentStateMachine.fallingState);
            else if(IsGrounded() == true)
                MoveMentStateMachine.ChangeState(MoveMentStateMachine.walkingState);
        }
        else
        {
            // 转入空闲状态时需要做的事情
            MoveMentStateMachine.reusableData.targetHSpeed = 0.0f;
            MoveMentStateMachine.player.rb2D.velocity = new Vector2(0, MoveMentStateMachine.player.rb2D.velocity.y);
            // 玩家没有移动输入，转入空闲状态
            if (IsGrounded() == false && IsFalling() == true)
                MoveMentStateMachine.ChangeState(MoveMentStateMachine.fallingState);
            else if (IsGrounded() == true)
                MoveMentStateMachine.ChangeState(MoveMentStateMachine.idlingState);
        }
    }

    public void SetDashVelocity()
    {
        var reusableData = MoveMentStateMachine.reusableData;
        var player = MoveMentStateMachine.player;

        // 获取当前水平速度
        float currentVelocityX = player.rb2D.velocity.x;
        float dashSpeed = reusableData.DashSpeed;
        float newVelocityX;

        // 判断当前速度是否为0
        if (Mathf.Abs(currentVelocityX) < 0.01f)
        {
            // 当前速度为0，根据角色朝向设定冲刺方向
            if (reusableData.facingRight)
            {
                newVelocityX = dashSpeed; // 向右冲刺，正数
            }
            else
            {
                newVelocityX = -dashSpeed; // 向左冲刺，负数
            }
        }
        else
        {
            // 当前速度不为0，根据当前速度方向叠加冲刺速度
            if (currentVelocityX > 0)
            {
                newVelocityX = currentVelocityX + dashSpeed; // 向右移动，叠加正冲刺速度
            }
            else
            {
                newVelocityX = currentVelocityX - dashSpeed; // 向左移动，叠加负冲刺速度
            }
        }

        // 应用新的速度
        Vector2 newVelocity = new Vector2(newVelocityX, player.rb2D.velocity.y);
        player.rb2D.velocity = newVelocity;

        // 更新当前速度记录和目标速度
        reusableData.currentHSpeed = newVelocityX;
    }
    /// <summary>
    /// 重写冲刺移动逻辑
    /// </summary>
    public override void MoveToTargetSpeed()
    {
    }
}
