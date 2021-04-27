using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaitForOrder2State : PlayerState
{
    public override void OnEnter(PlayerFMS player)
    {
        Debug.Log(player.gameObject.name + "is now waiting for order 2");
    }

    public override void Update(PlayerFMS player)
    {
        GraphUCS.instance.ClearInteractableTiles();
        PathDrawer.instance.DeletePath();
        //if(end) {
            Debug.Log(player.gameObject.name + "is now transitioning from Wait for Order 2 to End.");
            player.TransitionToState(player.endState);
        //}
    }

    public override void OnExit(PlayerFMS player)
    {
        throw new System.NotImplementedException();
    }
}
