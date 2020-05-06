using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRenderer : MonoBehaviour {
    private enum Materials {
        Idle,
        Current,
        Selectable,
        Attackable,
        Target
    }
    private TileStatus status;
    private MeshRenderer meshRenderer;
    public Material idleMaterial;
    public Material currentMaterial;
    public Material selectableMaterial;
    public Material attackableMaterial;
    public Material targetMaterial;
    void Start(){
        status = GetComponent<TileStatus>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    public void UpdateMaterial()
    {
        if(status.target) {
            meshRenderer.material = targetMaterial;
        }
        else if(status.current) {
            meshRenderer.material = currentMaterial;
        }
        else if(status.selectable) {
            meshRenderer.material = selectableMaterial;
        }
        else if(status.attackable) {
            meshRenderer.material = attackableMaterial;
        }
        else {
            meshRenderer.material = idleMaterial;
        }
    }
}
