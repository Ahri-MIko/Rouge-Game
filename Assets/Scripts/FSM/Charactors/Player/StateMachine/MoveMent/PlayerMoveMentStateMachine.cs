using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMentStateMachine : StateMachine
{
    public Player player { get; }

    public PlayerMoveReusableData reusableData { get;}
    public PlayerIdleingState idlingState { get; }

    public PlayerWalkingState walkingState { get; }

    public PlayerDashState dashState { get; }
    
    public PlayerJumpingState jumpingState { get; }

    public PlayerFallingState fallingState { get; }

    public PlayerNullState moveNullState { get; }

    public PlayerMoveMentStateMachine(Player p)
    {
        player = p;
        reusableData = p.MoveData; // 使用Player中的数据
        idlingState = new PlayerIdleingState(this);
        walkingState = new PlayerWalkingState(this);
        dashState = new PlayerDashState(this);
        jumpingState = new PlayerJumpingState(this);
        fallingState = new PlayerFallingState(this);
        moveNullState = new PlayerNullState(this);
    }

}
