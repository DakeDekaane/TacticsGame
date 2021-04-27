using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUIController : MonoBehaviour
{
    public static ActionUIController instance;

    void Awake() {
        instance = this;
    }

    public void EnableAttack() {
        ActiveCharacterManager.instance.readyForAttack = true;
    }
    public void EnableEnd() {
        ActiveCharacterManager.instance.activeUnit.turnController.EndTurn();
    }
}
