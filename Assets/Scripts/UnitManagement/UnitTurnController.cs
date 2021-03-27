﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTurnController : MonoBehaviour {
    public enum UnitState {
        Disabled,
        Standby,
        WaitForOrder1,
        Move,
        WaitForOrder2,
        Attack,
        UseItem,
        End,
    }

    public enum UnitSelectStatus {
        Selected,
        Unselected,
        Tapped
    }

    private bool moving {
        get {
            return GetComponent<UnitMovementController>().moving;
        }
    }
    public UnitSelectStatus selectStatus = UnitSelectStatus.Unselected;
    public UnitState state = UnitState.Disabled;
    public bool turn;

    void Update() {

        //If it's not their turn or already made their action, do nothing
        if(!turn || selectStatus == UnitSelectStatus.Tapped) {
            return;
        }


        //If player's turn, put units on standby
        else if(state == UnitState.Disabled && selectStatus != UnitSelectStatus.Tapped) {
            if(turn) {
                state = UnitState.Standby;
            }
        }

        //If player is active (selected), put them on wait for their first order.
        else if(state == UnitState.Standby) {
            if(ActiveCharacterManager.instance.activeUnit == GetComponent<Unit>()) {
                state = UnitState.WaitForOrder1;
            }
        }

        //If unit is waiting for their first order, choose the correct path depending on selected action
        else if(state == UnitState.WaitForOrder1) {
            Picker.instance.PickTarget();
            //Pick target tile, begin movement
            if(moving) {
                state = UnitState.Move;
            }
            //If no movement, we wait for further orders.
            if(ActiveCharacterManager.instance.selectedTile == ActiveCharacterManager.instance.targetTile) {
                state = UnitState.WaitForOrder2;
            }
            //Attack

            //Use item
            
        }

        //If unit is moving, wait for them to end their movement to put them on wait for the second order
        else if(state == UnitState.Move) {
            if(!moving) {
                state = UnitState.WaitForOrder2;
            }
        }

        //If unit is waiting for their second order, choose the correct path depending on selected action
        else if(state == UnitState.WaitForOrder2) {
            GraphUCS.instance.ClearInteractableTiles(); //Watch out for the behaviour of this function
            PathDrawer.instance.DeletePath();
            //Attack
            //Use item
            
            //!!
            state = UnitState.End;
            //!!
        }

        //If unit has attacked, end their turn
        else if(state == UnitState.Attack) {
            
        }

        //If unit has used an item, end their turn
        else if(state == UnitState.UseItem) {

        }

        //If unit has ended their action:
        //- Remove from turn list
        //- Disable them
        //- Tap it
        //- Set no active unit
        else if(state == UnitState.End) {
            TurnManager.instance.turnList.Remove(GetComponent<Unit>());
            state = UnitState.Disabled;
            selectStatus = UnitSelectStatus.Tapped;
            ActiveCharacterManager.instance.activeUnit = null;
        }
    }
}
