using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceText : MonoBehaviour
{
    public TileAStarData data;
    public TextMeshPro labelF;
    public TextMeshPro labelG;
    public TextMeshPro labelH;

    void Start() {
        data = GetComponent<TileAStarData>();
        labelF = transform.Find("Canvas/F").GetComponentInChildren<TextMeshPro>();
        labelG = transform.Find("Canvas/G").GetComponentInChildren<TextMeshPro>();
        labelH = transform.Find("Canvas/H").GetComponentInChildren<TextMeshPro>();
    }

    void Update() {
        if(data.f != 0) {
            labelF.text = "" + data.f;
            labelG.text = "" + data.g;
            labelH.text = "" + data.h;
        }
    }
}
