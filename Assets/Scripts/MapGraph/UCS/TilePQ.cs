using System;
using System.Collections.Generic;

public class TilePQ : IComparable<TilePQ>
  {
    public Tile tile;
    public int priority; // smaller values are higher priority

    public TilePQ(Tile tile, int priority)
    {
        this.tile = tile;
        this.priority = priority;
    }

    public override string ToString()
    {
        return "(" + tile + ", " + priority.ToString("F1") + ")";
    }
    public int CompareTo(TilePQ other)
    {
        if (this.priority < other.priority) return -1;
        else if (this.priority > other.priority) return 1;
        else return 0;
    }
} // Tile

