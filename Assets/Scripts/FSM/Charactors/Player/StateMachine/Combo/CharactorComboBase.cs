using UnityEngine;
using System.Collections.Generic;

public class CharactorComboBase
{

    protected Animator animator;

    protected PlayerReusableData reusableData;

    protected PlayerComboReusableData comboReusableData;
    
    // 添加对玩家Transform的引用
    protected Transform playerTransform;
    protected PlayerMoveReusableData moveData;

    public CharactorComboBase(Animator anim,PlayerReusableData PlayerRD,PlayerComboReusableData playerComboReusableData)
    {
        animator = anim;
        this.reusableData = PlayerRD;
        comboReusableData = playerComboReusableData;
        
        // 获取玩家Transform和移动数据
        if (anim != null)
        {
            playerTransform = anim.transform;
            // 尝试获取Player组件来访问移动数据
            Player player = anim.GetComponent<Player>();
            if (player != null)
            {
                moveData = player.MoveData;
                // 设置GameBlackboard中的玩家引用
                GameBlackboard.Instance.SetPlayerTransform(playerTransform);
            }
        }
    }

    #region 敌人检测
    
    
    
    /// <summary>
    /// 执行敌人检测
    /// </summary>
    public virtual void UpdateEnemys()
    {
        if (playerTransform == null) return;
        
        // 根据玩家朝向计算检测位置
        Vector2 detectionPosition = GetDetectionPosition();
        
        // 执行圆形检测 - 检测所有碰撞体
        Collider2D[] detectedColliders = Physics2D.OverlapCircleAll(detectionPosition, comboReusableData.detectionRadius);
        
        // 转换为GameObject列表，只保留Tag为"Enemy"的对象
        List<GameObject> detectedEnemies = new List<GameObject>();
        foreach (var collider in detectedColliders)
        {
            if (collider.gameObject != playerTransform.gameObject && // 排除玩家自己
                collider.gameObject.CompareTag("Enemy")) // 只检测Tag为"Enemy"的对象
            {
                detectedEnemies.Add(collider.gameObject);
            }
        }
        
        // 更新GameBlackboard中的敌人列表
        GameBlackboard.Instance.UpdateDetectedEnemies(detectedEnemies);
        
        // 调试输出
        if (detectedEnemies.Count > 0)
        {
            Debug.Log($"检测到 {detectedEnemies.Count} 个敌人");
        }
    }
    
    /// <summary>
    /// 根据玩家朝向计算检测位置
    /// </summary>
    /// <returns>检测位置</returns>
    private Vector2 GetDetectionPosition()
    {
        if (playerTransform == null || moveData == null) return Vector2.zero;
        
        Vector2 playerPosition = playerTransform.position;
        Vector2 offset = comboReusableData.detectionOffset;
        
        // 根据玩家朝向调整偏移 - 直接使用facingRight属性
        if (!moveData.facingRight)
        {
            offset.x = -offset.x; // 面向左时翻转X偏移
        }
        
        return playerPosition + offset;
    }
    

    /// <summary>
    /// 设置检测参数
    /// </summary>
    /// <param name="radius">检测半径</param>
    /// <param name="offset">检测偏移</param>
    public virtual void SetDetectionParameters(float radius, Vector2 offset)
    {
        comboReusableData.detectionRadius = radius;
        comboReusableData.detectionOffset = offset;
    }
    
    #endregion



    public virtual bool canComboInput()
    {
        //目前一直都设置可打击,后续细化可能要修改此处逻辑
        if (!reusableData.canInput) { return false; }
        return true;
    }


    public virtual void LightComboInput()
    { 
        ExecuteBaseCombo();
    }

    public virtual void ExecuteBaseCombo()
    {
        comboReusableData.hasATKCommand.Value = true;
        
       
    }

    public virtual void UpdateComboAnimation()
    {


        if(comboReusableData.hasATKCommand.Value && animator.GetBool(AnimatorID.isGrounded))
        {
            comboReusableData.isCharging.Value = true;
        }

        if(comboReusableData.isCharging.Value == true)
        {
            comboReusableData.chargeTime += Time.deltaTime;
            if (comboReusableData.chargeTime >= comboReusableData.chargeThreshold) Debug.Log("蓄力完成!!!");
                animator.CrossFade("ChargUp", 0.111f, 0);//进入蓄力状态
        }
        if(CharactorInputSystem.Instance.AttackWasReleasedThisFrame)
        {
            if (comboReusableData.chargeTime < comboReusableData.chargeThreshold && animator.GetBool(AnimatorID.isGrounded))
                animator.CrossFade("Attack1", 0.111f, 0);//进入普通攻击状态
            else
                comboReusableData.isChargeComplete.Value = true;

             comboReusableData.isCharging.Value = false;
            comboReusableData.chargeTime = 0.0f;
        }

        //空中的状态
        if(comboReusableData.hasATKCommand.Value && animator.GetBool(AnimatorID.isGrounded) == false && comboReusableData.InputY > 0)
        {
            animator.CrossFade("UpAttack", 0.1111f, 0);
        }
        if(animator.GetBool(AnimatorID.isGrounded) == false)
            comboReusableData.hasATKCommand.Value = false;
    }


}