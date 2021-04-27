using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackController : MonoBehaviour
{
    public bool attacking;
    public float hitAccuracy;
    public bool hit;
    public int damage;
    private UnitStatsController stats;

    void Start() {
        stats = GetComponent<UnitStatsController>();
    }
    
    //Calculate hit accuracy and if attack is going to be successful
    //Calculate damage
    public void Attack(Unit unit) {
        hitAccuracy = stats.currentAccuracy - unit.stats.currentAvoid;
        hit = Random.Range(0,100) < hitAccuracy;
        if (hit) {
            damage = stats.currentAtk - unit.stats.currentDef;
            if(damage < 0) {
                damage = 0;
            }
            unit.ReceiveDamage(damage);
            //Play attack animations ("Attack" on unit, "Hit" on enemy)
        }
        else {  
            //Play attack animations ("Attack" on unit, "Dodge" on enemy)
        }
    }

}
