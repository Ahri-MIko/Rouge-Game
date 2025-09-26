using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerComboReusableData
{
    [Header("攻击相关配置")]
    [SerializeField] public BindableProperty<bool> hasATKCommand = new BindableProperty<bool>();

    [Header("运行时数据 (只读)")]
    [SerializeField, ReadOnly] public float InputY;

}

  

