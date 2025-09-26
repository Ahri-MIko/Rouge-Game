using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 动画状态检测工具类
/// 用于方便地检测当前播放的动画状态
/// </summary>
public static class AnimationStateChecker
{
    // 预定义的动画状态哈希值（性能优化）
    private static readonly Dictionary<string, int> StateHashes = new Dictionary<string, int>
    {
        {"Idle", Animator.StringToHash("Base Layer.Idle")},
        {"Walk", Animator.StringToHash("Base Layer.walk")},
        {"Dash", Animator.StringToHash("Base Layer.DashLeft")},
        {"Jump", Animator.StringToHash("Base Layer.JumpStartLeft")},
        {"AirLoop", Animator.StringToHash("AirboneState.AirLoopLeft")},
        {"Attack1", Animator.StringToHash("Base Layer.Attack1")},
        {"UpAttack", Animator.StringToHash("Base Layer.UpAttack")}
    };

    /// <summary>
    /// 获取当前动画状态名称
    /// </summary>
    /// <param name="animator">动画控制器</param>
    /// <param name="layerIndex">动画层索引，默认为0</param>
    /// <returns>当前动画状态名称</returns>
    public static string GetCurrentStateName(Animator animator, int layerIndex = 0)
    {
        if (animator == null) return "None";
        
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        
        // 遍历预定义的状态哈希值
        foreach (var kvp in StateHashes)
        {
            if (stateInfo.fullPathHash == kvp.Value)
            {
                return kvp.Key;
            }
        }
        
        // 如果没有找到预定义的状态，返回完整路径
        return GetStateNameFromClipInfo(animator, layerIndex);
    }

    /// <summary>
    /// 检查是否正在播放指定动画
    /// </summary>
    /// <param name="animator">动画控制器</param>
    /// <param name="stateName">动画状态名称</param>
    /// <param name="layerIndex">动画层索引</param>
    /// <returns>是否正在播放指定动画</returns>
    public static bool IsPlayingAnimation(Animator animator, string stateName, int layerIndex = 0)
    {
        if (animator == null) return false;
        
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        
        // 方法1: 使用IsName检查（推荐用于简单状态名）
        if (stateInfo.IsName(stateName))
            return true;
            
        // 方法2: 使用哈希值检查（性能更好）
        if (StateHashes.ContainsKey(stateName))
        {
            return stateInfo.fullPathHash == StateHashes[stateName];
        }
        
        return false;
    }

    /// <summary>
    /// 获取当前动画的播放进度（0-1）
    /// </summary>
    /// <param name="animator">动画控制器</param>
    /// <param name="layerIndex">动画层索引</param>
    /// <returns>动画播放进度</returns>
    public static float GetCurrentAnimationProgress(Animator animator, int layerIndex = 0)
    {
        if (animator == null) return 0f;
        
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        return stateInfo.normalizedTime % 1f; // 取模确保在0-1范围内
    }

    /// <summary>
    /// 检查动画是否播放完成
    /// </summary>
    /// <param name="animator">动画控制器</param>
    /// <param name="layerIndex">动画层索引</param>
    /// <returns>动画是否播放完成</returns>
    public static bool IsAnimationComplete(Animator animator, int layerIndex = 0)
    {
        if (animator == null) return false;
        
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        return stateInfo.normalizedTime >= 1f && !stateInfo.loop;
    }

    /// <summary>
    /// 获取动画剪辑信息中的状态名称
    /// </summary>
    private static string GetStateNameFromClipInfo(Animator animator, int layerIndex)
    {
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(layerIndex);
        if (clipInfos.Length > 0)
        {
            return clipInfos[0].clip.name;
        }
        return "Unknown";
    }

    /// <summary>
    /// 获取详细的动画状态信息
    /// </summary>
    /// <param name="animator">动画控制器</param>
    /// <param name="layerIndex">动画层索引</param>
    /// <returns>动画状态信息字符串</returns>
    public static string GetDetailedAnimationInfo(Animator animator, int layerIndex = 0)
    {
        if (animator == null) return "Animator is null";
        
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
        string stateName = GetCurrentStateName(animator, layerIndex);
        float progress = GetCurrentAnimationProgress(animator, layerIndex);
        
        return $"State: {stateName}, Progress: {progress:F2}, Hash: {stateInfo.fullPathHash}";
    }
}