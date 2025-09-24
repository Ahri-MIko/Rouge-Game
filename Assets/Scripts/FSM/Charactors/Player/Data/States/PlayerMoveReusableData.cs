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
    
    [Header("转向设置")]
    [Range(0.0f, 1f)] public float facingThreshold = 0.1f; // 转向的最小速度阈值
    public bool useScaleFlip = true; // 是否使用Scale翻转
    
    [Header("运行时数据 (只读)")]
    [SerializeField, ReadOnly] public float targetHSpeed;
    [SerializeField, ReadOnly] public float currentHSpeed;
    [SerializeField, ReadOnly] public bool facingRight = false; // 当前面向方向

    [SerializeField,ReadOnly]public float InputX;
}

// 自定义只读属性特性
public class ReadOnlyAttribute : PropertyAttribute { }
