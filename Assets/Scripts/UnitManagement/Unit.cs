using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitStatsController stats;
    public UnitFaction faction;
    public UnitMovementController movementController;
    //public UnitTurnController turnController;
    public PlayerFMS turnController;
    public UnitAttackController attackController;

    public bool turn {
        get {
            //return GetComponent<UnitTurnController>().turn;
            return GetComponent<PlayerFMS>().turn;
        }
        set {
            //GetComponent<UnitTurnController>().turn = value;
            GetComponent<PlayerFMS>().turn = value;
        }
    }

    void Start()
    {
        stats = GetComponent<UnitStatsController>();
        movementController = GetComponent<UnitMovementController>();
        //turnController = GetComponent<UnitTurnController>();
        turnController = GetComponent<PlayerFMS>();
    }

    public void ReceiveDamage(int damage) {
        stats.SubstractHealth(damage);
        //UpdateHealthBar();
    }
}
