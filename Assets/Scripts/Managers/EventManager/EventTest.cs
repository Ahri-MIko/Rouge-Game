using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    private void Awake()
    {
        GameEventManager.Instance.AddEventListening("��Ծ������", onJumpPressed);
        GameEventManager.Instance.AddEventListening<Vector2>("�ƶ�������", onMovePressed);
    }

    public void Update()
    {
        if(CharactorInputSystem.Instance.Jump)
        {
            GameEventManager.Instance.CallBack("��Ծ������");
        }

        if(CharactorInputSystem.Instance.PlayerMove != Vector2.zero)
        {
            GameEventManager.Instance.CallBack("�ƶ�������", CharactorInputSystem.Instance.PlayerMove);
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
