using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatsController : MonoBehaviour
{
    public string characterName;

    public int maxHealth;
    public int baseAtk;
    public int baseDef;
    public int baseAccuracy;
    public int baseAvoid;

    public int currentHealth;
    public int currentAtk;
    public int currentDef;
    public int currentAccuracy;
    public int currentAvoid;

    public int movementPoints;
    public int attackRangeMax;
    public int attackRangeMin;

    public void SubstractHealth(int value) {
        currentHealth -= value;
        if(currentHealth < 0) {
            currentHealth = 0;
        }     
    }
    
}
