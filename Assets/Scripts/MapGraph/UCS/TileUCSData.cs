using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUCSData : MonoBehaviour {
    [System.Serializable]
    public class Data {
        public Tile parent;
        public bool visited;
        public int distance;

        public void Clear() {
            parent = null;
            visited = false;
            distance = 0;
        }
    };

    public Data selectableData = new Data();
    public Data attackableData = new Data();

    void Start(){
        selectableData.Clear();
        attackableData.Clear();
    }
    public void Reset(){
        selectableData.Clear();
        attackableData.Clear();
    }
}
