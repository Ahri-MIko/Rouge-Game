using UnityEngine;

public class AnimatorID : MonoBehaviour
{
    public static readonly int isGrounded = Animator.StringToHash("isGrounded");
    public static readonly int isWalking = Animator.StringToHash("isWalking");
    public static readonly int isDash = Animator.StringToHash("isDash");
}
