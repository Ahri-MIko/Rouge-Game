using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public BindableProperty<IState> currentState = new BindableProperty<IState>();

    public void ChangeState(IState nextState)
    {
        currentState.Value?.Exit();
        currentState.Value = nextState;
        currentState.Value?.Enter();
    }

    public  void HandInput()
    {
        currentState.Value?.HandInput();
    }

    public void Update()
    {
        currentState.Value?.Update();
    }

    public void OnAnimationTranslateEvent(IState state)
    {
        currentState.Value?.OnAnimationTranslateEvent(state);
    }

    public void OnAnimationExitEvent()
    {
        currentState.Value?.OnAnimationExitEvent();
    }
}
