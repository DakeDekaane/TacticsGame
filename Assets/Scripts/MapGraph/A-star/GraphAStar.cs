using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphAStar : MonoBehaviour
{
    public static GraphAStar instance;

    private List<Tile> openList = new List<Tile>();
    private List<Tile> closedList = new List<Tile>();
    [SerializeField]
    private List<Tile> tmpPath = new List<Tile>();
    [SerializeField]
    public Stack<Tile> path = new Stack<Tile>();

    private Tile tmpTile;
    [SerializeField]
    public Tile targetTile;

    public Unit targetUnit;

    void Start() {
        instance = this;
    }

    public void Reset() {
        tmpPath.Clear();
        path.Clear();
        closedList.Clear();
        openList.Clear();
    }

    //Pathfinding (A*)
    public void FindPath(Tile origin, Tile target) {
        origin.AStarData.Reset();
        target.AStarData.Reset();
        Reset();

        openList.Add(origin);

        //Gets target unit.
        targetTile = target;
        targetUnit = target.GetUnit();
        
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

            foreach(Tile t in tmpTile.graphData.adjacentTiles) {
                if(!t.terrain.walkable || closedList.Contains(t) || (t.GetUnit() && t.GetUnit() != targetUnit )){
                    continue;
                }

                int newMovemenCostToAdjacent = tmpTile.AStarData.g + GetDistance(tmpTile,t) + t.terrain.movementCost - 1;
                if( newMovemenCostToAdjacent < t.AStarData.g || !openList.Contains(t)) {
                    t.AStarData.g = newMovemenCostToAdjacent;
                    t.AStarData.h = GetDistance(t,target);
                    t.AStarData.parent = tmpTile;

                    if(!openList.Contains(t)) {
                        openList.Add(t);
                    }
                }
            }
        }
    }

    void RetracePath(Tile origin, Tile target) {
        tmpTile = target;

        //Fill list with complete path, target -> tiles -> origin
        while(tmpTile != origin) {
            tmpPath.Add(tmpTile);
            tmpTile = tmpTile.AStarData.parent;
        }

        //Discarding target tile if there's an unit on it.
        if (targetUnit) {
            tmpPath.Remove(targetTile);
        }

        //Discarding tiles out of range
        for(int i = 0; i < tmpPath.Count; i++) {
            if(!tmpPath[i].status.selectable) {
                continue;
            }
            tmpPath[i].status.target = true;
            tmpPath[i].renderer.UpdateMaterial();
            path.Push(tmpPath[i]);
        }
    }

    //Heuristic function 
    //Manhattan
    public int GetDistance(Tile origin, Tile destiny) {
        int dx = (int)Mathf.Abs(origin.transform.position.x - destiny.transform.position.x);
        int dy = (int)Mathf.Abs(origin.transform.position.z - destiny.transform.position.z);
        return dx + dy; //
    }
    //Euclidean 
    //public int GetDistance(Tile origin, Tile destiny) {
    //    int dx = (int)(origin.transform.position.x - destiny.transform.position.x);
    //    int dy = (int)(origin.transform.position.z - destiny.transform.position.z);
    //    return (int)Mathf.Sqrt(dx*dx + dy*dy);
    //}
}
