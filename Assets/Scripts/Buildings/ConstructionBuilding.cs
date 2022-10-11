using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionBuilding : MonoBehaviour
{
    [SerializeField] private List<Vector2Int> unwalkableNodes;
    [SerializeField] private GameObject finishedBuilding;
    [SerializeField] private float buildTime = 5.0f;
    private Vector2Int placedPos;
    public Vector2Int PlacedPos { get => placedPos; }

    private Action myAction;
    public List<Vector2Int> UnwalkableNodes { get => unwalkableNodes; }

    public virtual void OnBuildingPlaced(CustomGrid grid, Vector3 pos)
    {
        placedPos = new Vector2Int((int) pos.x, (int) pos.z);
        foreach (Vector2Int tile in UnwalkableNodes)
        {
            //Don't do this right now, cause it blocks the FindPathTask() if I do this before it's constructed
         //   grid.GetTile((int) pos.x + tile.y, (int) pos.z + tile.x).walkable = false;
            //TODO tell all units to recalc their path
        }
       TaskManager.instance.AddTask(new ConstructTask(myAction, this, finishedBuilding, buildTime));
        Debug.Log("Building is placed");
    }
}
