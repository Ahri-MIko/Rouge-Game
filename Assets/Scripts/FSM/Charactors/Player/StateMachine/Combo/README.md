# 连击系统使用说明

## 概述
连击系统是一个完整的状态机驱动的攻击系统，支持多段连击、输入缓冲、伤害计算等功能。

## 系统组成

### 1. 数据层 - PlayerComboReusableData
包含所有连击相关的配置参数：
- **连击基础设置**：最大连击数、时间窗口、重置时间
- **伤害设置**：基础伤害、每段连击的伤害倍数
- **动画设置**：每段连击的动画时长和恢复时间
- **移动设置**：连击时的移动速度限制
- **输入缓冲**：输入缓冲时间配置

### 2. 状态机层 - PlayerComboStateMachine
管理连击状态的切换：
- `PlayerComboIdleState`：连击空闲状态
- `PlayerComboAttackState`：连击攻击状态
- `PlayerComboRecoveryState`：连击恢复状态

### 3. 状态层
#### PlayerComboIdleState
- 等待攻击输入
- 只有在地面上才能开始连击
- 重置连击数据

#### PlayerComboAttackState
- 播放攻击动画
- 处理伤害计算
- 支持连击取消（在攻击动画60%后）
- 限制移动速度

#### PlayerComboRecoveryState
- 处理连击间隔
- 检查输入缓冲
- 决定是否继续连击或回到空闲状态

## 配置参数说明

### 连击基础设置
```csharp
[SerializeField] private int maxComboCount = 3;           // 最大连击数
[SerializeField] private float comboTimeWindow = 1.0f;    // 连击时间窗口
[SerializeField] private float comboResetTime = 2.0f;     // 连击重置时间
```

### 连击伤害设置
```csharp
[SerializeField] private float[] comboDamageMultipliers = {1.0f, 1.2f, 1.5f}; // 伤害倍数
[SerializeField] private float baseAttackDamage = 10f;                         // 基础伤害
```

### 连击动画设置
```csharp
[SerializeField] private float[] comboAnimationDurations = {0.5f, 0.6f, 0.8f}; // 动画时长
[SerializeField] private float[] comboRecoveryTimes = {0.2f, 0.3f, 0.5f};      // 恢复时间
```

### 连击移动设置
```csharp
[SerializeField] private float[] comboMovementSpeeds = {0.5f, 0.3f, 0.1f}; // 移动速度倍数
[SerializeField] private bool canMoveWhileAttacking = false;                // 是否允许攻击时移动
```

## 动画系统集成

### 需要的动画参数
在Animator Controller中添加以下参数：
- `isAttacking` (Bool)：是否正在攻击
- `attack` (Trigger)：攻击触发器
- `comboIndex` (Int)：当前连击段数

### 动画状态配置
为每个连击段创建动画状态，并在动画状态上添加`OnAnimationTranslation`脚本：
- 第1段连击：设置为`ComboAttack1`
- 第2段连击：设置为`ComboAttack2`
- 第3段连击：设置为`ComboAttack3`
- 恢复状态：设置为`ComboRecovery`

## 输入系统集成

### 输入映射
确保在Input Actions中定义了`Attack`输入：
```
PlayerInput -> Attack (例如：鼠标左键、J键等)
```

### 输入属性
系统提供以下输入属性：
- `CharactorInputSystem.Instance.Attack`：攻击触发
- `CharactorInputSystem.Instance.AttackPressed`：攻击按住
- `CharactorInputSystem.Instance.AttackWasPressedThisFrame`：本帧按下
- `CharactorInputSystem.Instance.AttackWasReleasedThisFrame`：本帧释放

## 使用流程

### 1. 配置数据
在Player组件的Inspector中配置连击参数：
- 设置最大连击数
- 调整每段连击的伤害倍数
- 配置动画时长和恢复时间
- 设置移动速度限制

### 2. 设置动画
- 创建连击动画剪辑
- 在Animator Controller中设置状态转换
- 为每个动画状态添加`OnAnimationTranslation`脚本

### 3. 配置输入
- 在Input Actions中添加Attack输入
- 绑定到合适的按键（如鼠标左键）

### 4. 测试连击
- 运行游戏
- 按下攻击键开始连击
- 在连击窗口内再次按下可继续连击
- 超时或达到最大连击数后自动重置

## 高级特性

### 输入缓冲
- 在攻击动画播放期间按下攻击键会被缓存
- 在恢复阶段会自动执行缓存的输入
- 缓冲时间可配置

### 连击取消
- 在攻击动画60%后可以取消到下一段连击
- 提供更流畅的连击体验
- 可以通过修改`canCancelCombo`的时机来调整

### 伤害检测
- 在攻击动画30%时进行伤害判定
- 自动检测攻击范围内的敌人
- 支持不同连击段的不同伤害倍数

### 移动限制
- 攻击时可以限制移动速度
- 每段连击可以有不同的移动速度倍数
- 可以完全禁止攻击时移动

## 扩展建议

### 添加新的连击段
1. 增加`maxComboCount`
2. 扩展伤害倍数、动画时长等数组
3. 创建对应的动画状态
4. 在`OnAnimationTranslation`中添加新的枚举值

### 添加特殊效果
1. 在`PlayerComboAttackState.DealDamage()`中添加特效
2. 在状态Enter/Exit中添加音效
3. 在伤害计算中添加暴击、元素伤害等

### 添加连击条件
1. 在`CanStartCombo()`中添加MP、体力等检查
2. 在`CanContinueCombo()`中添加特殊条件
3. 支持不同武器的不同连击模式

## 调试功能

系统提供了详细的Debug日志：
- 状态切换日志
- 连击段数显示
- 伤害计算日志
- 输入缓冲状态

可以通过Console查看系统运行状态，便于调试和优化。