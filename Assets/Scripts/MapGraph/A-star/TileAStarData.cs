using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAStarData : MonoBehaviour
{
    public int g = 0;
    public int h = 0;
    public Tile parent = null;
    public int distance;

    public int f {
        get {
            return g + h;
        }
    }

    public void Reset() {
        g = h = 0;
        parent = null;
        distance = 0 ;
    }

}
