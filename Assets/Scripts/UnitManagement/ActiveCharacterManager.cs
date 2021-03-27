using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterManager : MonoBehaviour
{
    public static ActiveCharacterManager instance;

    public Tile selectedTile;
    public Unit activeUnit;
    public bool ready;
    public bool moving {
        get {
            return activeUnit.movementController.moving;
        }
    }
    public Tile targetTile;

    void Start(){
        instance = this;
        ready = false;
    }

    void Update() {
        if (!activeUnit) {
            targetTile = null;
        }
    }
    
}
