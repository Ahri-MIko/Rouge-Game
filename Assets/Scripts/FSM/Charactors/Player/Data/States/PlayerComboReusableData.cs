using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerComboReusableData
{
    [Header("是否有攻击指令")]
    [SerializeField] public BindableProperty<bool> hasATKCommand = new BindableProperty<bool>();

    [Header("输入时数据 (只读)")]
    [SerializeField, ReadOnly] public float InputY;

    [Header("蓄力攻击配置")]
    [SerializeField] public float chargeThreshold = 0.1f; // 蓄力阈值时间（秒）
    [SerializeField] public BindableProperty<bool> isCharging = new BindableProperty<bool>(); // 是否正在蓄力
    [SerializeField] public BindableProperty<bool> isChargeComplete = new BindableProperty<bool>(); // 蓄力是否完成
   

    [Header("蓄力时数据 (只读)")]
    [SerializeField, ReadOnly] public float chargeTime = 0.0f; // 当前蓄力时间

}

  

