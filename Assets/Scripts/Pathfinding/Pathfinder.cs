using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Pathfinder : MonoBehaviour
{
    
    public static Pathfinder instance;
    [SerializeField]
    private GridManager gridManager;

    private void Awake()
    {
        instance = this;
    }



    public void FindPath(PathRequest request, Action<List<PathNode>, PathRequest, bool> callBack)
    {
        CustomGrid grid = gridManager.GetGrid();
        PathNode startNode = gridManager.GetGrid().GetTile(request.Start.x, request.Start.y).GetPathNode();
        PathNode endNode = gridManager.GetGrid().GetTile(request.End.x, request.End.y).GetPathNode();

        if (!startNode.IsWalkable() || !endNode.IsWalkable())
        {
            callBack(null, request, false);
            return;
        }
        
        PathfindingHeap<PathNode> openSet = new PathfindingHeap<PathNode>(grid.GetMaxSize());
        HashSet<PathNode> closedSet = new HashSet<PathNode>();
        openSet.Add(startNode);

        while (openSet.GetCount() > 0)
        {
            PathNode cur = openSet.RemoveFirst();
            closedSet.Add(cur);

            if (cur == endNode)
            {
                callBack(RetracePath(startNode, cur), request, true);
                return;
            }

            foreach (Tile neighbour in grid.GetMovableNeighbourTiles(grid.GetTile(cur.GetX(), cur.GetY())))
            {
                PathNode neighbourNode = neighbour.GetPathNode();
                if (!neighbourNode.IsWalkable() || closedSet.Contains(neighbourNode)) { continue; }

                int newMovementCostToNeighbour = cur.gCost + grid.GetDistance(cur.GetTile(), neighbour) + neighbourNode.movementPenalty;
                if (newMovementCostToNeighbour < neighbourNode.gCost || !openSet.Contains(neighbourNode))
                {
                    neighbourNode.gCost = newMovementCostToNeighbour;
                    neighbourNode.hCost = grid.GetDistance(neighbour, endNode.GetTile());
                    neighbourNode.fromNode = cur;

                    if (!openSet.Contains(neighbourNode))
                    {
                        openSet.Add(neighbourNode);
                    }
                    else
                    {
                        openSet.UpdateItem(neighbourNode);
                    }
                }
            }
        }
        callBack(null, request, false);
    }

    List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        PathNode cur = endNode;

        while (cur != startNode)
        {
            path.Add(cur);
            cur = cur.fromNode;
        }

        List<PathNode> waypoints = SimplifyPath(path);
        waypoints.Reverse();

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Debug.DrawLine(new Vector3(waypoints[i].GetX() + 0.5f, 0.1f, waypoints[i].GetY() + 0.5f) , new Vector3(waypoints[i + 1].GetX() + 0.5f, 0.1f, waypoints[i + 1].GetY() + 0.5f), Color.blue, 5f);
        }

        return waypoints;
    }

    List<PathNode> SimplifyPath(List<PathNode> path)
    {
        if (path.Count < 2) return path; 
        
        List<PathNode> wayPoints = new List<PathNode>();
        wayPoints.Add(path[0]);

        Vector2 oldDirection = Vector2.zero;
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector2 newDirection =
                new Vector2(path[i].GetX() - path[i + 1].GetX(), path[i].GetY() - path[i + 1].GetY());
            if (newDirection != oldDirection)
            {
                wayPoints.Add(path[i]);
            }

            oldDirection = newDirection;
        }
        wayPoints.Add(path[path.Count - 1]);
        return wayPoints;
    }
}
