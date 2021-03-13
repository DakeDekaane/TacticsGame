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

    public void GetInteractableTiles(Tile tile, Unit unit) {
        GetSelectableTiles(tile, unit);
        GetAttackableTiles(unit);
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
            t.status.target = false;
            t.renderer.UpdateMaterial();
        }
        selectableTiles.Clear();
    }

    void ClearAttackableTiles() {
         //Clear old data
        foreach(Tile t in attackableTiles) {
            t.searchData.attackableData.Clear();
            t.status.attackable = false;
            t.status.target = false;
            t.renderer.UpdateMaterial();
        }
        attackableTiles.Clear();
    }

    public void GetSelectableTiles(Tile tile, Unit unit) {
        
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
                if(tmpTile.searchData.selectableData.distance < unit.stats.movementPoints) {
                    foreach(Tile t in tmpTile.graphData.adjacentTiles) {
                        //Do we have movement points left?
                        if (tmpTile.searchData.selectableData.distance + t.terrain.movementCost <= unit.stats.movementPoints) {
                            //Is an unit on tile?
                            if (t.GetUnit() == null) {
                                if(!t.searchData.selectableData.visited) {
                                    t.searchData.selectableData.parent = tmpTile;
                                    t.searchData.selectableData.visited = true;
                                    t.searchData.selectableData.distance = tmpTile.searchData.selectableData.distance + t.terrain.movementCost;
                                    process.Enqueue(new TilePQ(t,t.searchData.selectableData.distance));
                                }
                            }
                        }
                    }
                    
                }
            }   
        }

        //Removing tiles occupied by allies.
        for(int i = selectableTiles.Count - 1; i >= 0; i--) {
            if (selectableTiles[i].GetUnit() != null) {
                if(tile == selectableTiles[i]) {
                continue;
                }
                if(selectableTiles[i].GetUnit().faction == unit.faction || unit.faction.allies.Contains(selectableTiles[i].GetUnit().faction)) {
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

    public void GetAttackableTiles(Unit unit) {
        ClearAttackableTiles();
        
        foreach(Tile tile in selectableTiles) {
            foreach(Tile adjTile in tile.graphData.adjacentTiles){
                if(!adjTile.status.selectable) {
                    processAttack.Enqueue(tile);
                    tile.searchData.attackableData.visited = true;
                    break;
                }
            }
        }

        while (processAttack.Count > 0) {
            tmpTile = processAttack.Dequeue();
            attackableTiles.Add(tmpTile);
            if( (tmpTile.terrain.walkable && tmpTile.GetUnit() == null) || ( tmpTile.GetUnit() != null && ! (unit.faction.allies.Contains(tmpTile.GetUnit().faction) || unit.faction == tmpTile.GetUnit().faction))) {
                tmpTile.status.attackable = true;
            }
            if(tmpTile.searchData.attackableData.distance < unit.stats.attackRangeMax) {
                foreach(Tile t in tmpTile.graphData.adjacentTiles) {
                    if(!t.searchData.attackableData.visited) {
                        t.searchData.attackableData.parent = tmpTile;
                        t.searchData.attackableData.visited = true;
                        t.searchData.attackableData.distance = tmpTile.searchData.attackableData.distance + 1;
                        processAttack.Enqueue(t);
                    }
                }
            }
        }
        
        foreach(Tile t in attackableTiles) {
            t.renderer.UpdateMaterial();
        }
    }


    public void GetAttackableTiles(Tile tile, Unit unit) {
        ClearAttackableTiles();

        processAttack.Enqueue(tile);
        tile.searchData.attackableData.visited = true;

        while (processAttack.Count > 0) {
            tmpTile = processAttack.Dequeue();
            attackableTiles.Add(tmpTile);
            tmpTile.status.attackable = true;
            if(tmpTile.searchData.attackableData.distance < (unit.stats.movementPoints + unit.stats.attackRangeMax)) {
                foreach(Tile t in tmpTile.graphData.adjacentTiles) {
                    if (tmpTile.searchData.attackableData.distance + t.terrain.movementCost <= unit.stats.movementPoints) {
                        if(!t.searchData.attackableData.visited || 
                            ((t.searchData.attackableData.distance != tmpTile.searchData.attackableData.distance + t.terrain.movementCost) && (t.searchData.attackableData.distance != tmpTile.searchData.attackableData.distance - tmpTile.terrain.movementCost))) {
                        t.searchData.attackableData.parent = tmpTile;
                        t.searchData.attackableData.visited = true;
                        t.searchData.attackableData.distance = tmpTile.searchData.attackableData.distance + t.terrain.movementCost;
                        processAttack.Enqueue(t);
                        }
                    }
                }
            }
        }
        for(int i = attackableTiles.Count - 1; i >= 0; i--) {
            if(attackableTiles[i].searchData.attackableData.distance < unit.stats.attackRangeMin) {
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
