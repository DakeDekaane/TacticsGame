using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnUI : MonoBehaviour
{
    public TextMeshProUGUI playerInTurn;

    void Update() {
        playerInTurn.text = TurnManager.instance.turnTeam.ToString() + " Turn";
    }
}
