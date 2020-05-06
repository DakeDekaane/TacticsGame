using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterFaction", menuName = "CharacterFaction", order = 52)]

public class CharacterFaction : ScriptableObject
{
    [SerializeField]
    public string _name;
    [SerializeField]
    public List<CharacterFaction> _allies;
    [SerializeField]
    public List<CharacterFaction> _enemies;

    public new string name {
        get {
            return _name;
        }
    }

    public List<CharacterFaction> allies {
        get {
            return _allies;
        }
    }

    public List<CharacterFaction> enemies {
        get {
            return _enemies;
        }
    }
}
