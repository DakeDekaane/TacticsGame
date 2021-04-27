using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFMS : MonoBehaviour {
    public Unit unit;   //Reference to Unit

    //States
    public readonly PlayerDisabledState disabledState = new PlayerDisabledState();
    public readonly PlayerStandbyState standbyState = new PlayerStandbyState();
    public readonly PlayerWaitForOrder1State waitForOrder1State = new PlayerWaitForOrder1State();
    public readonly PlayerMoveState moveState = new PlayerMoveState();
    public readonly PlayerWaitForOrder2State waitForOrder2State = new PlayerWaitForOrder2State();
    public readonly PlayerEndState endState = new PlayerEndState();
    private PlayerState currentState;

    //Selection Status
    public UnitSelectStatus selectStatus = UnitSelectStatus.Unselected;

    //Turn management
    public bool turn;
    public bool end;
    public bool moving {
        get {
            return GetComponent<UnitMovementController>().moving;
        }
    }
    public bool isTapped {
        get {
            return selectStatus == UnitSelectStatus.Tapped;
        }
    }
    void Start() {
        unit = GetComponent<Unit>();
        TransitionToState(disabledState);
    }

    void Update() {
        currentState.Update(this);
    }

    public void TransitionToState(PlayerState state) {
        currentState = state;
        currentState.OnEnter(this);
    }

    public void Unselect() {
        selectStatus = UnitSelectStatus.Unselected;
    }

    public void EndTurn() {
        end = true;
    }
}
