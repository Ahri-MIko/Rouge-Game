using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerComboReusableData
{
    [Header("�����������")]
    [SerializeField] public BindableProperty<bool> hasATKCommand = new BindableProperty<bool>();

    [Header("����ʱ���� (ֻ��)")]
    [SerializeField, ReadOnly] public float InputY;

}

  

