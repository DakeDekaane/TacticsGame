using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStatus : MonoBehaviour {
    //public bool walkable;
    public bool current;
    public bool selectable;
    public bool attackable;
    public bool target;

    void Start() {
        Reset();
    }

    public void Reset() {
        current = false;
        selectable = false;
        attackable = false;
        target = false;
    }
}
