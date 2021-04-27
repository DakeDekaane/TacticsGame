using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandbyState : PlayerState {
    public override void OnEnter(PlayerFMS player)
    {
        Debug.Log(player.gameObject.name + "is now on standby");
    }

    public override void Update(PlayerFMS player)
    {
        if(ActiveCharacterManager.instance.activeUnit == player.unit) {
            Debug.Log(player.gameObject.name + "is now transitioning from Standby to Wait for Order 1.");
            player.TransitionToState(player.waitForOrder1State);
        }
    }

    public override void OnExit(PlayerFMS player)
    {
        throw new System.NotImplementedException();
    }
}
