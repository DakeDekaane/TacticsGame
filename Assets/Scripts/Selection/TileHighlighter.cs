using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlighter : MonoBehaviour
{
    public GameObject highlighter;
    public Vector3 positionOffset;
    //public int distance;
    private Ray viewRay;
    private RaycastHit viewHit;
    private Tile tile;
    private int layerMask;
    private Vector3 lastMousePosition;

    void Start(){
        Debug.Log("TileLayer:" + LayerMask.NameToLayer("Tile"));
        layerMask = 1 << LayerMask.NameToLayer("Tile");
    }
    void Update()
    {
        if(Input.mousePosition != lastMousePosition) {
            viewRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(viewRay,out viewHit,Mathf.Infinity,layerMask) && (highlighter.transform.position != viewHit.transform.position + positionOffset)) {
                tile = viewHit.transform.GetComponent<Tile>();
                if(tile){
                    Debug.Log("Over tile: " + tile.name);
                    highlighter.transform.position = tile.transform.position + positionOffset;
                }
            }
        }
        lastMousePosition = Input.mousePosition;
    }
}
