using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour {
    public static Picker instance;
    [SerializeField] private Tile tmpTile;
    [SerializeField] private Tile originTile;
    [SerializeField] private Tile targetTile;
    private RaycastHit viewHit;
    private Ray viewRay;

    void Awake() {
        instance = this;
    }
    public void PickPlayer() {

        //If clicky but unit moving, do nothing
        if(Input.GetMouseButtonDown(0) && ActiveCharacterManager.instance.activeUnit && ActiveCharacterManager.instance.moving){
            return;
        }
        //Probably this will be moved to another script
        //Get the right clicky first
        //Draw ray and check if a tile has been clicked
        //Check if the tile has an unit on it
        //If there's an unit and it's their turn:
        //- Update status and material from the tile
        //- Set the selected character as active and ready for action (also get and show their selectable tiles)
        //If not, clear data (tiles status, active unit, readiness)
        if(Input.GetMouseButtonDown(0) && !ActiveCharacterManager.instance.ready){
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit)) {
                tmpTile = viewHit.transform.GetComponent<Tile>();
                if(tmpTile) {
                    if (tmpTile.GetUnit() && tmpTile.GetUnit().turn && tmpTile.GetUnit().turnController.selectStatus != UnitTurnController.UnitSelectStatus.Tapped) {
                        Debug.Log("Clicky on unit (1)");
                        originTile = tmpTile;
                        originTile.status.current = true;
                        originTile.renderer.UpdateMaterial();
                        ActiveCharacterManager.instance.selectedTile = originTile;
                        ActiveCharacterManager.instance.activeUnit = originTile.GetUnit();
                        GraphUCS.instance.GetInteractableTiles(ActiveCharacterManager.instance.selectedTile,ActiveCharacterManager.instance.activeUnit);
                        ActiveCharacterManager.instance.ready = true;
                    }
                }
            }
        }
        //If clicky on different unit (of the same faction)
        if(Input.GetMouseButtonDown(0) && ActiveCharacterManager.instance.ready){
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit)) {
                tmpTile = viewHit.transform.GetComponent<Tile>();
                if(tmpTile) {
                    if (tmpTile.GetUnit() && tmpTile.GetUnit().turn && tmpTile.GetUnit() != ActiveCharacterManager.instance.activeUnit) {
                        Debug.Log("Clicky on unit (2)");
                        PathDrawer.instance.DeletePath();
                        originTile = tmpTile;
                        originTile.status.current = true;
                        originTile.renderer.UpdateMaterial();
                        ActiveCharacterManager.instance.selectedTile = originTile;
                        ActiveCharacterManager.instance.activeUnit = originTile.GetUnit();
                        GraphUCS.instance.GetInteractableTiles(ActiveCharacterManager.instance.selectedTile,ActiveCharacterManager.instance.activeUnit);
                        ActiveCharacterManager.instance.ready = true;
                    }
                    //If clicky on tile outside range, unselect.
                    //Probably will be moved to another place
                    else if(!tmpTile.status.attackable && !tmpTile.status.selectable) {
                        Debug.Log("Clicky outside range");
                        ActiveCharacterManager.instance.activeUnit = null;
                        GraphUCS.instance.ClearInteractableTiles();
                        ActiveCharacterManager.instance.ready = false;
                        PathDrawer.instance.DeletePath();
                    }
                }
            }
        }
    }
    public void PickTarget() {
        //If unit selected and ready, get left clicky
        //Draw ray and check if a tile has been clicked and if it's selectable
        //Set selected tile as target and run the pathfinder.
        //Make unit follow the path
        if(ActiveCharacterManager.instance.ready && Input.GetMouseButtonDown(1)) {
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit)) {
                tmpTile = viewHit.transform.GetComponent<Tile>();
                if(tmpTile.status.selectable) {
                    Debug.Log("Clicky on target");
                    targetTile = tmpTile;
                    targetTile.status.target = true;
                    targetTile.renderer.UpdateMaterial();
                    ActiveCharacterManager.instance.targetTile = targetTile;
                    GraphAStar.instance.FindPath(ActiveCharacterManager.instance.selectedTile,ActiveCharacterManager.instance.targetTile);
                    ActiveCharacterManager.instance.ready = false;
                    StartCoroutine(ActiveCharacterManager.instance.activeUnit.movementController.FollowPath());
                    //
                }
            }
        }
        //If unit selected but no left clicky
        //Draw ray to tile on cursor
        //If the tile is different to the first tile
        //- Delete previous path and draw new path with nice arrows
        else if(ActiveCharacterManager.instance.activeUnit && ActiveCharacterManager.instance.ready) {
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit)) {
                tmpTile = viewHit.transform.GetComponent<Tile>();
                if(tmpTile != targetTile) {
                    PathDrawer.instance.DeletePath();
                    targetTile = tmpTile;
                    PathDrawer.instance.DrawPath(originTile,targetTile);
                }
            }
        }
    }
}