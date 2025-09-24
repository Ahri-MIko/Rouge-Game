using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    private void Awake()
    {
        GameEventManager.Instance.AddEventListening("跳跃键按下", onJumpPressed);
        GameEventManager.Instance.AddEventListening<Vector2>("移动键按下", onMovePressed);
    }

    public void Update()
    {
        if(CharactorInputSystem.Instance.Jump)
        {
            GameEventManager.Instance.CallBack("跳跃键按下");
        }

        if(CharactorInputSystem.Instance.PlayerMove != Vector2.zero)
        {
            GameEventManager.Instance.CallBack("移动键按下", CharactorInputSystem.Instance.PlayerMove);
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
