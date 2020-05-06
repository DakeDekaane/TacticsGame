using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterStats stats;
    public CharacterFaction faction;

    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }
}
