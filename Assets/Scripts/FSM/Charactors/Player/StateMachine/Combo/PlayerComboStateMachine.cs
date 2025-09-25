using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboStateMachine : StateMachine
{
    public Player player { get; }

    public PlayerComboReusableData reusableData { get; }

    public PlayerNormalAttack normalAttackState { get; }

    public PlayerComboNullState NullState { get; }  



    public PlayerComboStateMachine(Player p)
    {
        player = p;

        reusableData = player.ComboData;

        normalAttackState = new PlayerNormalAttack(this);

        NullState = new PlayerComboNullState(this);
    }
}
