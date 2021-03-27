using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGraphData : MonoBehaviour
{
    [SerializeField] public List<Tile> adjacentTiles;
    
    void Start(){
        InitAdjacentTiles();
    }

    void InitAdjacentTiles(){
        InitAdjacentTilesInDirection(Vector3.forward);
        InitAdjacentTilesInDirection(Vector3.back);
        InitAdjacentTilesInDirection(Vector3.left);
        InitAdjacentTilesInDirection(Vector3.right);
    }

    void InitAdjacentTilesInDirection(Vector3 direction) {
        Collider[] adjacentColliders = Physics.OverlapBox(transform.position + direction * 2.0f, transform.localScale * 0.1f);
        foreach(Collider c in adjacentColliders) {
            Tile tmpTile = c.GetComponent<Tile>();
            if (tmpTile){
                adjacentTiles.Add(tmpTile);
            }
        }
    }
}
