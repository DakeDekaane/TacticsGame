using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAStarData : MonoBehaviour
{
    public float f = 0;
    public float g = 0;
    public float h = 0;
    public Tile parent = null;

    public void Reset() {
        f = g = h = 0;
        parent = null;
    }

}
