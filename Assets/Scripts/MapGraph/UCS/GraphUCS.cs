using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphUCS : MonoBehaviour
{
    public static GraphUCS instance;
    private PriorityQueue<TilePQ> process = new PriorityQueue<TilePQ>();
    private Queue<Tile> processAttack = new Queue<Tile>();
    private List<Tile> selectableTiles = new List<Tile>();
    private List<Tile> attackableTiles = new List<Tile>();

    private Tile tmpTile;

    void Start() {
        instance = this;
    }

    public void GetInteractableTiles(Tile tile, Character character) {
        GetSelectableTiles(tile, character);
        GetAttackableTiles(character);
    }

    public void ClearInteractableTiles() {
        ClearSelectableTiles();
        ClearAttackableTiles();
    }
    void ClearSelectableTiles(){
        //Clear old data
        foreach(Tile t in selectableTiles) {
            t.searchData.selectableData.Clear();
            t.status.current = false;
            t.status.selectable = false;
            t.renderer.UpdateMaterial();
        }
        selectableTiles.Clear();
    }

    void ClearAttackableTiles() {
         //Clear old data
        foreach(Tile t in attackableTiles) {
            t.searchData.attackableData.Clear();
            t.status.attackable = false;
            t.renderer.UpdateMaterial();
        }
        attackableTiles.Clear();
    }

    public void GetSelectableTiles(Tile tile, Character character) {
        
        ClearSelectableTiles();

        //Search Algorithm (UCS)
        process.Enqueue(new TilePQ(tile,0));
        tile.status.current = true;
        tile.searchData.selectableData.visited = true;
        while (process.Count() > 0) {
            tmpTile = process.Dequeue().tile;
            //Can we walk on tile?
            if(tmpTile.terrain.walkable ) {
                selectableTiles.Add(tmpTile);
                tmpTile.status.selectable = true;
                //Is tile on our range?
                if(tmpTile.searchData.selectableData.distance < character.stats.movementPoints) {
                    foreach(TileTransition t in tmpTile.graphData.adjacentTiles) {
                        //Do we have movement points left?
                        if (tmpTile.searchData.selectableData.distance + t.cost <= character.stats.movementPoints) {
                            //Is an enemy on tile?
                            if (t.tile.GetCharacter() == null || !character.faction.enemies.Contains(t.tile.GetCharacter().faction)) {
                                if(!t.tile.searchData.selectableData.visited) {
                                    t.tile.searchData.selectableData.parent = tmpTile;
                                    t.tile.searchData.selectableData.visited = true;
                                    t.tile.searchData.selectableData.distance = tmpTile.searchData.selectableData.distance + t.cost;
                                    process.Enqueue(new TilePQ(t.tile,t.tile.searchData.selectableData.distance));
                                }
                            }
                        }
                    }
                    
                }
            }   
        }

        //Removing tiles occupied by allies.
        for(int i = selectableTiles.Count - 1; i >= 0; i--) {
            if (selectableTiles[i].GetCharacter() != null) {
                if(tile == selectableTiles[i]) {
                continue;
                }
                if(selectableTiles[i].GetCharacter().faction == character.faction || character.faction.allies.Contains(selectableTiles[i].GetCharacter().faction)) {
                    selectableTiles[i].status.selectable = false;
                    selectableTiles[i].status.current = false;
                    selectableTiles[i].searchData.selectableData.Clear();
                    selectableTiles.Remove(selectableTiles[i]);
                }
            }            
            
        }
        //Painting tiles
        foreach(Tile t in selectableTiles) {
            t.renderer.UpdateMaterial();
        }
    }

    public void GetAttackableTiles(Character character) {
        ClearAttackableTiles();
        
        foreach(Tile tile in selectableTiles) {
            if(tile.searchData.selectableData.distance == character.stats.movementPoints) {
                processAttack.Enqueue(tile);
                tile.searchData.attackableData.visited = true;
            }
        }

        while (processAttack.Count > 0) {
            tmpTile = processAttack.Dequeue();
            attackableTiles.Add(tmpTile);
            if( (tmpTile.terrain.walkable && tmpTile.GetCharacter() == null) || ( tmpTile.GetCharacter() != null && ! (character.faction.allies.Contains(tmpTile.GetCharacter().faction) || character.faction == tmpTile.GetCharacter().faction))) {
                tmpTile.status.attackable = true;
            }
            if(tmpTile.searchData.attackableData.distance < character.stats.attackRangeMax) {
                foreach(TileTransition t in tmpTile.graphData.adjacentTiles) {
                    if(!t.tile.searchData.attackableData.visited) {
                        t.tile.searchData.attackableData.parent = tmpTile;
                        t.tile.searchData.attackableData.visited = true;
                        t.tile.searchData.attackableData.distance = tmpTile.searchData.attackableData.distance + 1;
                        processAttack.Enqueue(t.tile);
                    }
                }
            }
        }
        
        foreach(Tile t in attackableTiles) {
            t.renderer.UpdateMaterial();
        }
    }


    public void GetAttackableTiles(Tile tile, Character character) {
        ClearAttackableTiles();

        processAttack.Enqueue(tile);
        tile.searchData.attackableData.visited = true;

        while (processAttack.Count > 0) {
            tmpTile = processAttack.Dequeue();
            attackableTiles.Add(tmpTile);
            tmpTile.status.attackable = true;
            if(tmpTile.searchData.attackableData.distance < (character.stats.movementPoints + character.stats.attackRangeMax)) {
                foreach(TileTransition t in tmpTile.graphData.adjacentTiles) {
                    if (tmpTile.searchData.attackableData.distance + t.cost <= character.stats.movementPoints) {
                        if(!t.tile.searchData.attackableData.visited || 
                            ((t.tile.searchData.attackableData.distance != tmpTile.searchData.attackableData.distance + t.cost) && (t.tile.searchData.attackableData.distance != tmpTile.searchData.attackableData.distance - tmpTile.terrain.movementCost))) {
                        t.tile.searchData.attackableData.parent = tmpTile;
                        t.tile.searchData.attackableData.visited = true;
                        t.tile.searchData.attackableData.distance = tmpTile.searchData.attackableData.distance + t.cost;
                        processAttack.Enqueue(t.tile);
                        }
                    }
                }
            }
        }
        for(int i = attackableTiles.Count - 1; i >= 0; i--) {
            if(attackableTiles[i].searchData.attackableData.distance < character.stats.attackRangeMin) {
                attackableTiles[i].status.attackable = false;
                attackableTiles[i].searchData.attackableData.Clear();
                attackableTiles.Remove(attackableTiles[i]);
            }
        }
        foreach(Tile t in attackableTiles) {
            t.renderer.UpdateMaterial();
        }
    }
}
