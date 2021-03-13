using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour {
    public static PathDrawer instance;
    private GameObject arrow;
    private Vector3 directionFrom;
    private Vector3 directionTo;
    private List<GameObject> arrows = new List<GameObject>();
    [SerializeField] private Vector3 positionOffset;
    private List<Tile> path;

    void Awake() {
        instance = this;
    }
    public void DeletePath() {
        //Hide all the visible arrows (they go back to the arrow pool)
        foreach(GameObject obj in arrows) {
            obj.SetActive(false);
        }
        arrows.Clear();
    }
    public void DrawPath(Tile originTile, Tile targetTile) {
        if(originTile == targetTile) { // No movement, no arrow
            return;
        }

        //Getting list of tiles. !!!May need optimizing!!!
        GraphAStar.instance.FindPath(originTile,targetTile);
        path = new List<Tile>(GraphAStar.instance.drawPath);
        path.Reverse();
        
        //This is where the fun begins
        for (int i = 0; i < path.Count ; ++i) {
            if(i == 0) {  //End Tile, draw arrow point
                directionFrom = GetDirection(path.Count == 1 ? originTile : path[i + 1], path[i]);
                arrow = PathArrowPool.instance.getItem("End");
                arrow.transform.forward = directionFrom;
            }
            else {
                directionFrom = GetDirection(i == path.Count - 1 ? originTile: path[i + 1], path[i]);
                directionTo = GetDirection(path[i],path[i - 1]);
                if (directionFrom == directionTo) { //Straight Path
                    arrow = PathArrowPool.instance.getItem("Straight");
                    arrow.transform.forward = directionFrom;
                }
                else { //Curves
                    arrow = PathArrowPool.instance.getItem("Curve");
                    if ((directionFrom == Vector3.left && directionTo == Vector3.forward) || (directionFrom == Vector3.back && directionTo == Vector3.right)) {
                        arrow.transform.forward = Vector3.forward; 
                    }
                    else if ((directionFrom == Vector3.left && directionTo == Vector3.back) || (directionFrom == Vector3.forward && directionTo == Vector3.right)) {
                        arrow.transform.forward = Vector3.right; 
                    }
                    else if ((directionFrom == Vector3.right && directionTo == Vector3.back) || (directionFrom == Vector3.forward && directionTo == Vector3.left)) {
                        arrow.transform.forward = Vector3.back; 
                    }
                    else if ((directionFrom == Vector3.right && directionTo == Vector3.forward) || (directionFrom == Vector3.back && directionTo == Vector3.left)) {
                        arrow.transform.forward = Vector3.left; 
                    }
                }
            }
            arrow.transform.position = path[i].transform.position + positionOffset;
            arrow.SetActive(true);
            arrows.Add(arrow);
        }
    }

    public Vector3 GetDirection(Tile originTile, Tile targetTile){
        return (targetTile.transform.position - originTile.transform.position).normalized;
    }
}
