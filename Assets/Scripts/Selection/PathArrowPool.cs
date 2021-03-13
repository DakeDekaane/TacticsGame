using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathArrowPool : MonoBehaviour
{
    public static PathArrowPool instance;
    public List<GameObject> straightPathPool;
    public List<GameObject> curvePathPool;
    public List<GameObject> endPathPool;
    [SerializeField] private GameObject straightPath;
    [SerializeField] private GameObject curvePath;
    [SerializeField] private GameObject endPath;
    [SerializeField] private int straightPathAmount;
    [SerializeField] private int curvePathAmount;
    [SerializeField] private int endPathAmount;

    void Awake() {
        instance = this;
    }

    void Start() {
        straightPathPool = CreatePool(straightPath,straightPathAmount);
        curvePathPool = CreatePool(curvePath,curvePathAmount);
        endPathPool = CreatePool(endPath,endPathAmount);
    }

    private List<GameObject> CreatePool(GameObject item, int amount) {
        List<GameObject> pool = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amount; ++i) {
            tmp = Instantiate(item);
            tmp.SetActive(false);
            pool.Add(tmp);
        }
        return pool;
    }

    public GameObject getItem(string type){
        if(type == "Straight"){
            for(int i = 0; i < straightPathAmount; ++i) {
                if (!straightPathPool[i].activeInHierarchy) {
                    return straightPathPool[i];
                }
            }
        }
        if(type == "End"){
            for(int i = 0; i < endPathAmount; ++i) {
                if (!endPathPool[i].activeInHierarchy) {
                    return endPathPool[i];
                }
            }
        }
        if(type == "Curve"){
            for(int i = 0; i < curvePathAmount; ++i) {
                if (!curvePathPool[i].activeInHierarchy) {
                    return curvePathPool[i];
                }
            }
        }
        return null;
    }
}
