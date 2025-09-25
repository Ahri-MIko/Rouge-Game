using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorInputSystem : Singleton<CharactorInputSystem>
{
    public CharacterInputs inputActions;

    protected override void Awake()
    {
        base.Awake();
        if(inputActions == null)
        {
            inputActions = new CharacterInputs();
        }
    }

    private void OnEnable()
    {
        inputActions?.Enable();
    }



    private void OnDisable()
    {
        inputActions?.Disable(); 
    }

    

    public Vector2 PlayerMove
    {
        get => inputActions.PlayerInput.Move.ReadValue<Vector2>();
    }

    public bool Jump
    {
        get=>inputActions.PlayerInput.Jump.triggered;
    }
    
    public bool JumpPressed
    {
        get => inputActions.PlayerInput.Jump.IsPressed();
    }
    
    public bool JumpWasPressedThisFrame
    {
        get => inputActions.PlayerInput.Jump.WasPressedThisFrame();
    }
    
    public bool JumpWasReleasedThisFrame
    {
        get => inputActions.PlayerInput.Jump.WasReleasedThisFrame();
    }

    public bool Dash
    {
        get=>inputActions.PlayerInput.Dash.triggered;
    }
    
    public bool Attack
    {
        get => inputActions.PlayerInput.Attack.triggered;
    }
    
    public bool AttackPressed
    {
        get => inputActions.PlayerInput.Attack.IsPressed();
    }
    
    public bool AttackWasPressedThisFrame
    {
        get => inputActions.PlayerInput.Attack.WasPressedThisFrame();
    }
    
    public bool AttackWasReleasedThisFrame
    {
        get => inputActions.PlayerInput.Attack.WasReleasedThisFrame();
    }





}

