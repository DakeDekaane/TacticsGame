using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UnitFaction", menuName = "UnitFaction", order = 52)]

public class UnitFaction : ScriptableObject
{
    [SerializeField] public string _name;
    [SerializeField] public List<UnitFaction> _allies;
    [SerializeField] public List<UnitFaction> _enemies;

    public new string name {
        get {
            return _name;
        }
    }

    public List<UnitFaction> allies {
        get {
            return _allies;
        }
    }

    public List<UnitFaction> enemies {
        get {
            return _enemies;
        }
    }
}
