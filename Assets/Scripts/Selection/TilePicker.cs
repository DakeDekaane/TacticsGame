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
        void Update() {
        //Get the right clicky first
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
        //If unit selected and ready, get left clicky
        //Draw ray and check if a tile has been clicked
        //Set selected tile as target and run the pathfinder.
        //Make unit follow the path
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
        //If unit selected but no left clicky
        //Draw ray to tile on cursor
        //If the tile is different to the first tile
        //- Delete previous path and draw new path with nice arrows
        else if(ActiveCharacterManager.instance.ready) {
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit)) {
                nextTile2 = viewHit.transform.GetComponent<Tile>();
                if(nextTile2 != tile2) {
                    PathDrawer.instance.DeletePath();
                    tile2 = nextTile2;
                    PathDrawer.instance.DrawPath(tile,tile2);
                }
            }
        }
    }


    
}