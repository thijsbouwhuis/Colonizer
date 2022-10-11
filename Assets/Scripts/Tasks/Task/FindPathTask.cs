using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class FindPathTask : TaskBase 
{
    private Queue<PathNode> curPath = new Queue<PathNode>();
    private PathNode curTarget;
    private Action<Queue<PathNode>, bool> pathRequestCallback;
    private Vector2Int start;
    private Vector2Int end;
    private float maxMoveSpeed;

    public FindPathTask(Action callback, Vector2Int start, Vector2Int end, float maxMoveSpeed) : base(callback)
    {
        this.maxMoveSpeed = maxMoveSpeed;
        this.start = start;
        this.end = end;
        pathRequestCallback += OnPathRequestCallback;
    }
    
    
    public override void StartTask()
    {
        PathRequestManager.RequestPath(new PathRequest(start, end, pathRequestCallback));
    }
    
    public override void UpdateTask()  
    {
        //Assign next target along path
        if (curTarget == null && curPath.Count > 0)
        {
            curTarget = curPath.Dequeue();
        }
            
        //Move along path
        if (curTarget != null) { MoveTo(curTarget); }

        if (curTarget == null && curPath.Count <= 0)
        {
            OnTaskFinished();
        }
    }

    public override void OnTaskFinished()
    {
        base.OnTaskFinished();
        Debug.Log("Arrived At Destination");
    }
    
    private void OnPathRequestCallback(Queue<PathNode> path, bool success)
    {
        if (success) { curPath = path; }
        else { Debug.Log("Couldn't find a path properly."); }
    }

    private void MoveTo(PathNode target)
    {
        var position = manager.gameObject.transform.position;
        var targetDestination = new Vector3(target.GetX(), position.y, target.GetY());
        var destination = Vector3.MoveTowards(position, targetDestination, maxMoveSpeed);
        manager.gameObject.transform.position = destination;
            
        if (Vector3.Distance(destination, targetDestination) < 0.05f)
        {
            manager.gameObject.transform.position = targetDestination;
            curTarget = null;
        }
    }
}
