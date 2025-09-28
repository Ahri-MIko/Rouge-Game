using UnityEngine;

public class AnimatorID : MonoBehaviour
{
    public static readonly int isGrounded = Animator.StringToHash("isGrounded");
    public static readonly int isWalking = Animator.StringToHash("isWalking");
    public static readonly int isDash = Animator.StringToHash("isDash");
    public static readonly int isJumping = Animator.StringToHash("isJumping");
    public static readonly int isFalling = Animator.StringToHash("isFalling");
    public static readonly int ishaveInputX = Animator.StringToHash("ishaveInputX");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int HasAttack = Animator.StringToHash("HasAttack");
    public static readonly int UpPressed = Animator.StringToHash("UpPressed");
    
    // 蓄力攻击相关参数
    public static readonly int IsCharging = Animator.StringToHash("isCharge"); // 是否正在蓄力  ChargUp  bool
    public static readonly int ChargeComplete = Animator.StringToHash("ChargeFinish"); // 蓄力是否完成 ChargeAttack  bool
    public static readonly int ChargeAttack = Animator.StringToHash("ChargeAttack"); // 蓄力攻击触发器 trigger



}
