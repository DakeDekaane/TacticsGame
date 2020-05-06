using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphAStar : MonoBehaviour
{
    public static GraphAStar instance;

    private List<Tile> openList = new List<Tile>();
    private List<Tile> closedList = new List<Tile>();
    [SerializeField]
    private List<Tile> path = new List<Tile>();

    private Tile tmpTile;
    [SerializeField]
    public Tile actualTargetTile;

    void Start() {
        instance = this;
    }

    public void FindPath(Tile origin, Tile target) {
        origin.AStarData.Reset();
        target.AStarData.Reset();

        openList.Add(origin);
        
        while(openList.Count > 0) {
            tmpTile = openList[0];
            for (int i = 1; i < openList.Count; i++) {
                if (openList[i].AStarData.f < tmpTile.AStarData.f || openList[i].AStarData.f == tmpTile.AStarData.f && openList[i].AStarData.h < tmpTile.AStarData.h) {
                    tmpTile = openList[i];
                }
            }

            openList.Remove(tmpTile);
            closedList.Add(tmpTile);

            if (tmpTile == target) {
                RetracePath(origin,target);
                return;
            }

            foreach(TileTransition t in tmpTile.graphData.adjacentTiles) {
                if(!t.tile.terrain.walkable || closedList.Contains(t.tile)){
                    continue;
                }

                int newMovemenCostToAdjacent = tmpTile.AStarData.g + GetDistance(tmpTile,t.tile);
                if( newMovemenCostToAdjacent < t.tile.AStarData.g || !openList.Contains(t.tile)) {
                    t.tile.AStarData.g = newMovemenCostToAdjacent;
                    t.tile.AStarData.h = GetDistance(t.tile,target);
                    t.tile.AStarData.parent = tmpTile;

                    if(!openList.Contains(t.tile)) {
                        openList.Add(t.tile);
                    }
                }
            }
        }
    }

    void RetracePath(Tile origin, Tile target) {
        tmpTile = target;

        while(tmpTile != origin) {
            tmpTile.status.target = true;
            tmpTile.renderer.UpdateMaterial();
            path.Add(tmpTile);
            tmpTile = tmpTile.AStarData.parent;
        }
        path.Reverse();
    }

    //Heuristic function
    public int GetDistance(Tile origin, Tile destiny) {
        int dx = (int)Mathf.Abs(origin.transform.position.x - destiny.transform.position.x);
        int dy = (int)Mathf.Abs(origin.transform.position.z - destiny.transform.position.z);
        return dx + dy;
    }
}
