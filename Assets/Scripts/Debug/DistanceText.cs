using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceText : MonoBehaviour
{
    public TileUCSData data;
    public TextMeshPro label;

    void Start() {
        data = GetComponent<TileUCSData>();
        label = transform.Find("Canvas/Text").GetComponentInChildren<TextMeshPro>();
    }

    void Update() {
        if(data.attackableData.distance > 0) {
            label.text = "" + data.attackableData.distance;
            label.gameObject.SetActive(true);
        }
    }
}
