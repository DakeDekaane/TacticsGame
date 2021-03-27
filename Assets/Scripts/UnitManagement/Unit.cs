using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitStatsController stats;
    public UnitFaction faction;
    public UnitMovementController movementController;
    public UnitTurnController turnController;

    public bool turn {
        get {
            return GetComponent<UnitTurnController>().turn;
        }
        set {
            GetComponent<UnitTurnController>().turn = value;
        }
    }

    void Start()
    {
        stats = GetComponent<UnitStatsController>();
        movementController = GetComponent<UnitMovementController>();
        turnController = GetComponent<UnitTurnController>();
    }
}
