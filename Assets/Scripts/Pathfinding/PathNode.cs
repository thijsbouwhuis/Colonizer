using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : IPathfindingHeapItem<PathNode>
{
    private Tile parent;
    public int gCost;
    public int hCost;
    public int movementPenalty = 5;
    public PathNode fromNode;
    public int index;
    public int fCost => gCost + hCost;

    public PathNode(Tile parent)
    {
        this.parent = parent;
    }

    public int GetX()
    {
        return parent.x;
    }

    public int GetY()
    {
        return parent.y;
    }
    public bool IsWalkable()
    {
        return parent.walkable;
    }

    public Tile GetTile()
    {
        return parent;
    }

    public int Index
    {
        get => index;
        set => index = value;
    }

    public int CompareTo(PathNode compareNode)
    {
        int compare = fCost.CompareTo(compareNode.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(compareNode.hCost);
        }

        return -compare;
    }

}
