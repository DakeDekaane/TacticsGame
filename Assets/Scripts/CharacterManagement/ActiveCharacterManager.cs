using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterManager : MonoBehaviour
{
    public static ActiveCharacterManager instance;

    public Character activeCharacter;

    void Start(){
        instance = this;
    }
    
}
