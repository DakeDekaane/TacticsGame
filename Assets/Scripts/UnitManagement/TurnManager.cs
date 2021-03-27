using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum Faction {
        Player,
        Ally,
        Enemy
    }

    public static TurnManager instance;
    
    [SerializeField] private GameObject[] playerUnits;
    [SerializeField] private GameObject[] allyUnits;
    [SerializeField] private GameObject[] enemyUnits;

    [SerializeField] private List<Unit> playerList = new List<Unit>();
    [SerializeField] private List<Unit> allyList = new List<Unit>();
    [SerializeField] private List<Unit> enemyList = new List<Unit>();

    public List<Unit> turnList = new List<Unit>();

    public Faction turnTeam = Faction.Enemy; //Turn switch behaviour makes this begin with player turn.
    public bool allies;

    public Unit activeUnit;

    void Awake() {
        instance = this;
    }

    void Start() {
        InitTeamLists();
    }

    void Update() {
        
        //If turn list is empty, it means a player has finished their turn, 
        //So we prepare the units for the next player's turn
        if(turnList.Count == 0) {
            turnTeam = GetNextTeam();
            if(turnTeam == Faction.Player) {
                PrepareTurn(playerList);
            }
            else if(turnTeam == Faction.Ally) {
                PrepareTurn(allyList);
                //StartTurn();

            }
            else if (turnTeam == Faction.Enemy) {
                PrepareTurn(enemyList);
                //StartTurn();
            }
        }

        //If player's turn, call Picker to receive player commands
        //if(turnTeam == Faction.Player) {
            Picker.instance.PickPlayer();
        //}
    }

    public void PrepareTurn(List<Unit> list) {
        //Give turn and unselect units units and add them to the turn list
        //No need to clear as it should be guaranteed is empty beforehand.
        foreach(Unit unit in list) {
            //if(turnTeam == Faction.Player) {
                unit.turn = true;
                unit.turnController.selectStatus = UnitTurnController.UnitSelectStatus.Unselected;
                //unit.selected = false;
            //}
            turnList.Add(unit);
        }
    }

    public void InitTeamLists() {
        //Search for objects and put them in a list
        playerUnits = GameObject.FindGameObjectsWithTag("Player");
        FillList(playerList,playerUnits);
        if(allies) {
            allyUnits = GameObject.FindGameObjectsWithTag("Ally");
            FillList(allyList,allyUnits);
        }
        enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
        FillList(enemyList,enemyUnits);
    }

    void FillList(List<Unit> list, GameObject[] array) {
        list.Clear();
        foreach(GameObject unit in array) {
            list.Add(unit.GetComponent<Unit>());
        }
    }

    public void EndTurn() {
        turnList.Clear();
    }

    Faction GetNextTeam() {
        if(turnList.Count == 0) {
            if(turnTeam == Faction.Player) {
                if(allies) {
                    return Faction.Ally;
                }
                else {
                    Debug.Log("Enemy Turn");
                    return Faction.Enemy;
                }
            }
            else if (turnTeam == Faction.Ally) {
                return Faction.Enemy;
            }
            else if(turnTeam == Faction.Enemy) {
                Debug.Log("Player Turn");
                return Faction.Player;
            }
        }
        return turnTeam;  
    }
}
