using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackStateBase : IState
{

    public PlayerComboStateMachine ComboStateMachine;
    protected PlayerReusableData playerRD;

    protected PlayerComboReusableData comboReusableData;
    protected Animator anim;

    protected CharactorComboBase CharactorComboBase;


    public PlayerAttackStateBase(PlayerComboStateMachine comboStateMachine )
    {
        ComboStateMachine = comboStateMachine;

        if(playerRD == null)
        {
            playerRD = comboStateMachine.player.ReusableData;
        }

        if(comboReusableData == null)
        {
            comboReusableData = comboStateMachine.player.ComboData;
        }

        if(anim == null)
        {
            anim = comboStateMachine.player.animator;
        }

        if(CharactorComboBase == null)
        {
            CharactorComboBase = new CharactorComboBase(anim, playerRD, comboReusableData);
        }
    }

    #region StateMachine Interface
    public virtual void Enter()
    {
        Debug.Log("攻击状态机进入" + GetType().Name +"状态");
        AddInputActionEvent();
    }

    public virtual void Exit()
    {
        RemoveInputActionEvent();
    }

    public virtual void HandInput()
    {
        Vector2 moveInput = CharactorInputSystem.Instance.PlayerMove;
        comboReusableData.InputY = moveInput.y;
        anim.SetBool(AnimatorID.UpPressed, moveInput.y > 0 ? true : false);

    }

    public virtual void OnAnimationExitEvent()
    {
        
    }

    public virtual void OnAnimationTranslateEvent(IState state)
    {
        ComboStateMachine.ChangeState(state);
    }

    public virtual void Update()
    {
        CharactorComboBase.UpdateComboAnimation();
    }

    #endregion

    protected virtual void AddInputActionEvent()
    {
        CharactorInputSystem.Instance.inputActions.PlayerInput.Attack.started += OnAttackStart;
        AddBindableAction();
    }

    private void AddBindableAction()
    {
        comboReusableData.hasATKCommand.onValueChanged += (value) =>
        {
            anim.SetBool(AnimatorID.HasAttack, value);
        };
    }

   
    protected virtual void RemoveInputActionEvent()
    {
        CharactorInputSystem.Instance.inputActions.PlayerInput.Attack.started -= OnAttackStart;
        RemoveBindableAction();

    }
    private void RemoveBindableAction()
    {
        comboReusableData.hasATKCommand.onValueChanged = null;
    }

    private void OnAttackStart(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
        // 其他情况下的正常攻击处理
        anim.SetTrigger(AnimatorID.Attack);
        if(CharactorComboBase.canComboInput())
        {
            CharactorComboBase.LightComboInput();
        }
    }
}
