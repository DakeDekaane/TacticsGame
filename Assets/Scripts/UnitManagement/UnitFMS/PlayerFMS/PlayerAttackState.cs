using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public override void OnEnter(PlayerFMS player)
    {
        Debug.Log(player.gameObject.name + "is now attacking");
    }

    public override void Update(PlayerFMS player)
    {
        
    }

    public override void OnExit(PlayerFMS player)
    {
        throw new System.NotImplementedException();
    }
}
