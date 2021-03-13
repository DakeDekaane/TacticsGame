using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePicker : MonoBehaviour {
    [SerializeField]
    private Tile tile;
    [SerializeField]
    private Tile tile2; //tmp
    [SerializeField]
    private Tile nextTile2;
    private RaycastHit viewHit;
    private Ray viewRay;
    [SerializeField]
    private GameObject arrow;
    public Vector3 directionFrom;
    public Vector3 directionTo;
    public List<GameObject> arrows = new List<GameObject>();
    public Vector3 offset;
    public List<Tile> path;
    void Update() {
        //Get the clicky first
        //Draw ray and check if a tile has been clicked
        //Check if the tile has an unit on it
        //If there's an unit :
        //- Update status and material from the tile
        //- Set the selected character as active and ready for action (also get and show their selectable tiles)
        //If not, clear data (tiles status, active unit, readiness)
        if(Input.GetMouseButtonDown(0)){
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit)) {
                tile = viewHit.transform.GetComponent<Tile>();
                if(tile) {
                    if (tile.GetUnit()) {
                        tile.status.current = true;
                        tile.renderer.UpdateMaterial();
                        ActiveCharacterManager.instance.selectedTile = tile;
                        ActiveCharacterManager.instance.activeUnit = tile.GetUnit();
                        GraphUCS.instance.GetInteractableTiles(ActiveCharacterManager.instance.selectedTile,ActiveCharacterManager.instance.activeUnit);
                        ActiveCharacterManager.instance.ready = true;
                    }
                    //Probably will be moved to another place
                    else if(!tile.status.attackable && !tile.status.selectable) {
                        ActiveCharacterManager.instance.activeUnit = null;
                        GraphUCS.instance.ClearInteractableTiles();
                        ActiveCharacterManager.instance.ready = false;
                    }
                    //
                }
            }
        }
        if(ActiveCharacterManager.instance.ready && Input.GetMouseButtonDown(1)) {
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit)) {
                tile = viewHit.transform.GetComponent<Tile>();
                if(tile) {
                    tile.status.target = true;
                    tile.renderer.UpdateMaterial();
                    ActiveCharacterManager.instance.targetTile = tile;
                    GraphAStar.instance.FindPath(ActiveCharacterManager.instance.selectedTile,ActiveCharacterManager.instance.targetTile);
                    ActiveCharacterManager.instance.ready = false;
                    StartCoroutine(ActiveCharacterManager.instance.activeUnit.movement.FollowPath());
                    //
                }
            }
        }
        else if(ActiveCharacterManager.instance.ready) {
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit)) {
                nextTile2 = viewHit.transform.GetComponent<Tile>();
                if(nextTile2 != tile2) {
                    DeletePath();
                    tile2 = nextTile2;
                    DrawPath(tile,tile2);
                }
            }
        }
    }

    void DeletePath() {
        foreach(GameObject obj in arrows) {
            obj.SetActive(false);
        }
        arrows.Clear();
    }
    void DrawPath(Tile originTile, Tile targetTile) {
        if(originTile == targetTile) { // No movement, no arrow
            return;
        }

        //Getting list of tiles.
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
            arrow.transform.position = path[i].transform.position + offset;
            arrow.SetActive(true);
            arrows.Add(arrow);
        }
    }

    public Vector3 GetDirection(Tile originTile, Tile targetTile){
        return (targetTile.transform.position - originTile.transform.position).normalized;
    }
    
}