using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileTransition {
        public Tile tile;
        public int cost;

        public TileTransition(Tile tile, int cost) {
            this.tile = tile;
            this.cost = cost;
        }
    }
