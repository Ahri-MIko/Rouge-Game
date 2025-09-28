using System.Collections.Generic;
using UnityEngine;


public class GameBlackboard : SingletonNoMono<GameBlackboard>
{
    [Header("敌人检测数据")]
    public List<GameObject> detectedEnemies = new List<GameObject>();
    
    [Header("玩家数据")]
    public Transform playerTransform;
    
    /// <summary>
    /// 更新检测到的敌人列表
    /// </summary>
    /// <param name="enemies">检测到的敌人列表</param>
    public void UpdateDetectedEnemies(List<GameObject> enemies)
    {
        detectedEnemies.Clear();
        detectedEnemies.AddRange(enemies);
    }
    
    /// <summary>
    /// 添加检测到的敌人
    /// </summary>
    /// <param name="enemy">敌人对象</param>
    public void AddDetectedEnemy(GameObject enemy)
    {
        if (!detectedEnemies.Contains(enemy))
        {
            detectedEnemies.Add(enemy);
        }
    }
    
    /// <summary>
    /// 移除检测到的敌人
    /// </summary>
    /// <param name="enemy">敌人对象</param>
    public void RemoveDetectedEnemy(GameObject enemy)
    {
        detectedEnemies.Remove(enemy);
    }
    
    /// <summary>
    /// 清空检测到的敌人列表
    /// </summary>
    public void ClearDetectedEnemies()
    {
        detectedEnemies.Clear();
    }
    
    /// <summary>
    /// 获取检测到的敌人数量
    /// </summary>
    /// <returns>敌人数量</returns>
    public int GetDetectedEnemyCount()
    {
        return detectedEnemies.Count;
    }
    
    /// <summary>
    /// 设置玩家Transform引用
    /// </summary>
    /// <param name="player">玩家Transform</param>
    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }
}