using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMoveReusableData
{

    [Header("冲刺速度设置")]
    [Range(0f, 100f)] public float DashSpeed = 80.0f;
    [Header("移动速度设置")]
    [Range(1f, 20f)] public float maxWalkSpeed = 5f;
    [Range(1f, 30f)] public float maxRunSpeed = 8f;
    
    [Header("加速度设置")]
    [Range(5f, 50f)] public float acceleration = 20f;
    [Range(5f, 50f)] public float deceleration = 20f;
    
    [Header("跳跃设置")]
    [Range(5f, 30f)] public float shortJumpForce = 12f; // 短按跳跃力度
    [Range(10f, 40f)] public float longJumpForce = 18f; // 长按跳跃力度
    [Range(0.1f, 1f)] public float longPressThreshold = 0.3f; // 长按时间阈值
    [Range(0.1f, 2f)] public float jumpBufferTime = 0.2f; // 跳跃缓冲时间
    [Range(0.1f, 2f)] public float coyoteTime = 0.15f; // 土狼时间（离开地面后仍可跳跃的时间）
    
    [Header("转向设置")]
    [Range(0.0f, 1f)] public float facingThreshold = 0.1f; // 转向的最小速度阈值
    public bool useScaleFlip = true; // 是否使用Scale翻转
    
    [Header("运行时数据 (只读)")]
    [SerializeField, ReadOnly] public float targetHSpeed;
    [SerializeField, ReadOnly] public float currentHSpeed;
    [SerializeField, ReadOnly] public bool facingRight = false; // 当前面向方向
    [SerializeField, ReadOnly] public float InputX;
    
    [Header("跳跃运行时数据 (只读)")]
    [SerializeField, ReadOnly] public bool isJumping = false; // 是否正在跳跃
    [SerializeField, ReadOnly] public float jumpPressTime = 0f; // 跳跃键按下时间
    [SerializeField, ReadOnly] public float lastGroundedTime = 0f;
    [SerializeField, ReadOnly] public float lastJumpPressTime = -10f; 
}

// 自定义只读属性特性
public class ReadOnlyAttribute : PropertyAttribute { }
