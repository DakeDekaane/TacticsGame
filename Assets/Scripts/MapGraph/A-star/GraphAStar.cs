using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphAStar : MonoBehaviour
{
    public static GraphAStar instance;

    private List<Tile> openList = new List<Tile>();
    private List<Tile> closedList = new List<Tile>();

    private Stack<Tile> path = new Stack<Tile>();

    private Tile tmpTile;
    [SerializeField]
    public Tile actualTargetTile;

    void Start() {
        instance = this;
    }

    public Tile FindLowestF(List<Tile> list) {
        Tile lowest = list[0];
        foreach(Tile tile in list) {
            if(tile.AStarData.f < lowest.AStarData.f) {

            }
        }
        list.Remove(lowest);
        return lowest;
    }

    public Tile FindEndTile(Tile target){
        Stack<Tile> tmpPath = new Stack<Tile>();
        Tile next = target.AStarData.parent;
        
        while(next != null) {
            tmpPath.Push(next);
            next = next.AStarData.parent;
        }

        if(tmpPath.Count <= next.searchData.selectableData.distance) {
            return target.AStarData.parent;
        }

        Tile endTile =  null;
        for(int i = 0; i <= next.searchData.selectableData.distance; i++) {
            endTile = tmpPath.Pop();
        }
        return endTile;
    }

    //This probably doesn't belong here
    public void MoveToTile(Tile tile) {
        path.Clear();
        tile.status.target = true;
        Tile next = tile;
        while (next != null) {
            path.Push(next);
            next = next.AStarData.parent;
        }
    }

    public void FindPath(Tile origin, Tile target) {
        origin.AStarData.Reset();
        target.AStarData.Reset();
        
        openList.Add(origin);

        origin.AStarData.h = Vector3.Distance(origin.transform.position,target.transform.position);
        origin.AStarData.f = origin.AStarData.h;

        while(openList.Count > 0) {
            tmpTile = FindLowestF(openList);
            closedList.Add(tmpTile);
            if(tmpTile == target) {
                Tile actualtargetTile = FindEndTile(tmpTile);
                MoveToTile(actualTargetTile);
                return;
            }
            else {

            }
            foreach(TileTransition t in tmpTile.graphData.adjacentTiles) {
                if( closedList.Contains(t.tile)) {

                }
                else if(openList.Contains(t.tile)) {
                    float tempG = origin.AStarData.g + Vector3.Distance(t.tile.transform.position,tmpTile.transform.position);
                    if (tempG < t.tile.AStarData.g) {
                        t.tile.AStarData.parent = tmpTile;
                        t.tile.AStarData.g = tempG;
                        t.tile.AStarData.f = t.tile.AStarData.g + t.tile.AStarData.h;
                    }
                }
                else {
                    t.tile.AStarData.parent = tmpTile;
                    t.tile.AStarData.g = tmpTile.AStarData.g + Vector3.Distance(t.tile.transform.position,tmpTile.transform.position);
                    t.tile.AStarData.h = Vector3.Distance(t.tile.transform.position,target.transform.position);
                    t.tile.AStarData.f = t.tile.AStarData.h + t.tile.AStarData.g;
                    openList.Add(t.tile);
                }
            }
        }
    }
}
