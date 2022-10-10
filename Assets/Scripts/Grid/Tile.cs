using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private PathNode pathNode;
    public int x;
    public int y;
    public bool walkable = true;

    public Tile(int x, int y)
    {
        this.x = x;
        this.y = y;
        Initialize();
    }


    private void Initialize()
    {
        pathNode = new PathNode(this);
    }

    public PathNode GetPathNode()
    {
        return pathNode;
    }
}
