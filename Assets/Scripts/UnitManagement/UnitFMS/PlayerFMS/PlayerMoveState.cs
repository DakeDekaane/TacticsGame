using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public override void OnEnter(PlayerFMS player)
    {
        Debug.Log(player.gameObject.name + "is now moving");
    }

    public override void Update(PlayerFMS player)
    {
        if(!player.moving) {
            Debug.Log(player.gameObject.name + "is now transitioning from Move to Wait for Order 2.");
            player.TransitionToState(player.waitForOrder2State);
        }
    }

    public override void OnExit(PlayerFMS player)
    {
        throw new System.NotImplementedException();
    }
}
