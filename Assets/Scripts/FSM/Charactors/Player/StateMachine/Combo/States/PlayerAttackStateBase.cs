using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackStateBase : IState
{

    PlayerComboStateMachine ComboStateMachine;

    public PlayerAttackStateBase(PlayerComboStateMachine comboStateMachine )
    {
        ComboStateMachine = comboStateMachine;
    }

    public virtual void Enter()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Exit()
    {
        throw new System.NotImplementedException();
    }

    public virtual void HandInput()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnAnimationExitEvent()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnAnimationTranslateEvent(IState state)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}
