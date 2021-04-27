using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndState : PlayerState
{
    public override void OnEnter(PlayerFMS player)
    {
        Debug.Log(player.gameObject.name + "is now ending its turn");
    }

    public override void Update(PlayerFMS player)
    {
        TurnManager.instance.turnList.Remove(player.unit);
        player.selectStatus = UnitSelectStatus.Tapped;
        ActiveCharacterManager.instance.activeUnit = null;
        player.end = false;
        Debug.Log(player.gameObject.name + "is now transitioning from End to Disabled.");
        player.TransitionToState(player.disabledState);
    }

    public override void OnExit(PlayerFMS player)
    {
        throw new System.NotImplementedException();
    }
}
