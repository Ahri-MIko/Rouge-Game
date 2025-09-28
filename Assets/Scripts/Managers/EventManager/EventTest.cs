using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{

    [SerializeField]private float holdTime = 0.0f;
    [SerializeField]private bool isPressed = false;
    [SerializeField] private float maxholdTime = 0.1f;
    private void Awake()
    {
       
    }

    public void Update()
    {
        if (isPressed)
        {
            holdTime += Time.deltaTime;
            if(holdTime > maxholdTime)
            {
                Debug.Log("正在蓄力了");
            }
        }
        if (CharactorInputSystem.Instance.AttackWasPressedThisFrame)
        {
            holdTime += Time.deltaTime;
            isPressed = true;
        }


        if(CharactorInputSystem.Instance.AttackWasReleasedThisFrame)
        {
            if(holdTime > maxholdTime)
            {
                Debug.Log("蓄力完成");
            }
            else
            {
                Debug.Log("短按完成");
            }
            holdTime = 0.0f;
            isPressed= false;
            
        }
    }
    public void onJumpPressed()
    {
        Debug.Log("Jump has been pressed");
    }

    public void onMovePressed(Vector2 move)
    {
        Debug.Log(move);

    }
}
