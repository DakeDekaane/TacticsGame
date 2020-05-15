using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterManager : MonoBehaviour
{
    public static ActiveCharacterManager instance;

    public Tile selectedTile;
    public Unit activeUnit;
    public bool ready;
    public Tile targetTile;

    void Start(){
        instance = this;
        ready = false;
    }
    
}
