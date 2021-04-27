using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaitForOrder1State : PlayerState
{
    public override void OnEnter(PlayerFMS player)
    {
        Debug.Log(player.gameObject.name + "is now waiting for order 1");
    }

    public override void Update(PlayerFMS player)
    {
        Picker.instance.PickTargetForMovement();
        if(player.moving) {
            Debug.Log(player.gameObject.name + "is now transitioning from Wait for Order 1 to Move.");
            player.TransitionToState(player.moveState);
        }
    }

    public override void OnExit(PlayerFMS player)
    {
        throw new System.NotImplementedException();
    }
}
