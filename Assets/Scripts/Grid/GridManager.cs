using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Grid gridComponent;
    public CustomGrid grid;

    Action<List<PathNode>, bool> pathResult;

    private void Start()
    {
        grid = new CustomGrid(20, 20, gridComponent.cellSize);
        pathResult += GetPathResult;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
          Vector3 target = BuildingSystem.instance.SnapCoordinateToGrid(BuildingSystem.instance.GetMouseWorldPos());
          PathRequestManager.RequestPath(new PathRequest(new Vector2Int(0, 0), new Vector2Int((int)target.x, (int)target.z), pathResult));
        }
    }

    private void GetPathResult(List<PathNode> path, bool success)
    {
        if (success) { Debug.Log("Path found successfully");}
        if (!success) { Debug.Log("Couldn't find a path properly.");}
        int x = 01;
    }
    public CustomGrid GetGrid()
    {
        return grid;
    }
}
