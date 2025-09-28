using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimationTranslation : StateMachineBehaviour
{
    Player player;

    public enum OnEnterAnimationPlayerState
    {
        Idle,
        Walk,
        Dash,
        Jump,
        ATK,
        ChargeUp,      // 蓄力状态
        ChargeAttack,  // 蓄力攻击状态
        Null
    }

    [SerializeField] public OnEnterAnimationPlayerState onEnterAnimationState;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.TryGetComponent<Player>(out player))
        {
            player.OnAnimationTranslateEvent(onEnterAnimationState);
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent<Player>(out player))
        {
            player.OnAnimationExitEvent();
        }
    }

}
