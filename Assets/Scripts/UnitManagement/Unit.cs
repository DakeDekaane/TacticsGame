using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitStats stats;
    public UnitFaction faction;
    public UnitMovement movement;

    void Start()
    {
        stats = GetComponent<UnitStats>();
        movement = GetComponent<UnitMovement>();
    }
}
