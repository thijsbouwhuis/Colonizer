using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private List<Vector2Int> unwalkableNodes;

    private Action myAction;
    public List<Vector2Int> UnwalkableNodes { get => unwalkableNodes; }

    public virtual void OnBuildingPlaced(CustomGrid grid, Vector3 pos)
    {
        foreach (Vector2Int tile in UnwalkableNodes)
        {
            //Don't do this right now, cause it blocks the FindPathTask() if I do this before it's constructed
         //   grid.GetTile((int) pos.x + tile.y, (int) pos.z + tile.x).walkable = false;
            //TODO tell all units to recalc their path
        }
       TaskManager.instance.AddTask(
            new FindPathTask(myAction, new Vector2Int(0, 0), new Vector2Int((int)pos.x, (int)pos.z), 0.1f));
        Debug.Log("Building is placed");
    }
}
