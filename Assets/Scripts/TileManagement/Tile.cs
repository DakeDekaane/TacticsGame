using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile: MonoBehaviour {
    public TileStatus status;
    public new TileRenderer renderer;
    public TileGraphData graphData;
    public TileUCSData searchData;
    public TileAStarData AStarData;
    public Character currentCharacter;
    public TerrainData terrain;

    void Start(){
        status = GetComponent<TileStatus>();
        renderer = GetComponent<TileRenderer>();
        graphData = GetComponent<TileGraphData>();
        searchData = GetComponent<TileUCSData>();
        AStarData = GetComponent<TileAStarData>();
    }

    private RaycastHit viewHit;

    public Character GetCharacter() {
        if (Physics.Raycast(transform.position, Vector3.up, out viewHit, 1)) {
            if(viewHit.transform.tag == "Player" || viewHit.transform.tag == "Enemy" || viewHit.transform.tag == "Ally") {
                return viewHit.transform.GetComponent<Character>();
            }
        }
        return null;
    }
}
