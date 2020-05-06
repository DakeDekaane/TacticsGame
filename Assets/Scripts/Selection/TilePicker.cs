using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePicker : MonoBehaviour {
    [SerializeField]
    private Tile tile;
    [SerializeField]
    private Tile tile2; //tmp
    private RaycastHit viewHit;
    private Ray viewRay;
    
    void Update() {
        if(Input.GetMouseButtonDown(0)){
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit)) {
                tile = viewHit.transform.GetComponent<Tile>();
                if(tile) {
                    if (tile.GetCharacter()) {
                        tile.status.current = true;
                        tile.renderer.UpdateMaterial();
                        ActiveCharacterManager.instance.selectedTile = tile;
                         ActiveCharacterManager.instance.activeCharacter = tile.GetCharacter();
                        GraphUCS.instance.GetInteractableTiles(ActiveCharacterManager.instance.selectedTile,ActiveCharacterManager.instance.activeCharacter);
                        ActiveCharacterManager.instance.ready = true;
                    }
                    //Probably will be moved to another place
                    else if(!tile.status.attackable && !tile.status.selectable) {
                        ActiveCharacterManager.instance.activeCharacter = null;
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
                    //
                }
            }
        }
        
    }
}
