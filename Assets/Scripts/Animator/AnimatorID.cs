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
    

}
