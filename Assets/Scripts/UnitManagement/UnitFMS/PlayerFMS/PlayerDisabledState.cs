using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisabledState : PlayerState {
    
    public override void OnEnter(PlayerFMS player)
    {
        Debug.Log(player.gameObject.name + "is now disabled");
    }

    public override void Update(PlayerFMS player)
    {
        if(player.selectStatus != UnitSelectStatus.Tapped && player.turn) {
            Debug.Log(player.gameObject.name + "is now transitioning from Disabled to Standby.");
            player.TransitionToState(player.standbyState);
        }
    }

    public override void OnExit(PlayerFMS player)
    {
        throw new System.NotImplementedException();
    }
}
