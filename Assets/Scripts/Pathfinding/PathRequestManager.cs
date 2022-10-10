using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    private Queue<PathResult> pathResults = new Queue<PathResult>();

    public static PathRequestManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (pathResults.Count > 0) {
            int itemsInQueue = pathResults.Count;
            lock (pathResults)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = pathResults.Dequeue();
                    result.PathRequest.Callback(result.Path, result.Success);
                }
            }
        }
    }

    public static void RequestPath(PathRequest pathRequest)
    {
        ThreadStart threadStart = delegate { Pathfinder.instance.FindPath(pathRequest, instance.FinishProcessingPath); };
        threadStart.Invoke();
    }

    private void FinishProcessingPath(List<PathNode> path, PathRequest pathRequest, bool success)
    {
        
        lock (pathResults)
        {
            pathResults.Enqueue(new PathResult(path, pathRequest, success));
        }
    }
}


