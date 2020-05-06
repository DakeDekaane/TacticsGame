using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePicker : MonoBehaviour {
    private Tile tile;
    private RaycastHit viewHit;
    private Ray viewRay;
    
    void Update() {
        if(Input.GetMouseButtonDown(0)){
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit)) {
                tile = viewHit.transform.GetComponent<Tile>();
                if(tile) {
                    ActiveCharacterManager.instance.activeCharacter = tile.GetCharacter();
                    if (ActiveCharacterManager.instance.activeCharacter) {
                        tile.status.current = true;
                        tile.renderer.UpdateMaterial();
                        GraphUCS.instance.GetInteractableTiles(tile,ActiveCharacterManager.instance.activeCharacter);
                    }
                    //Probably will be moved to another place
                    else if(!tile.status.attackable && !tile.status.selectable) {
                        ActiveCharacterManager.instance.activeCharacter = null;
                        GraphUCS.instance.ClearInteractableTiles();
                    }
                    //
                }
            }
        }
    }
}
