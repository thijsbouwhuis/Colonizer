using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unit
{
    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] private float maxMoveSpeed = 0.1f;

        [SerializeField] private GridManager gridManager;
        private Queue<PathNode> curPath = new Queue<PathNode>();
        private PathNode curTarget;
        
        Action<Queue<PathNode>, bool> pathRequestCallback;

        private bool requestedPath;
        private void Start() { pathRequestCallback += GetPathResult; }
        
        void FixedUpdate()
        {
            //Assign next target along path
            if (curTarget == null && curPath.Count > 0)
            {
                curTarget = curPath.Dequeue();
            }
            
            //Move along path
            if (curTarget != null) { MoveTo(curTarget); }

            //TODO Temporary test code, randomly get paths
            if (curPath.Count == 0 && !requestedPath)
            {
                requestedPath = true;
                PathRequestManager.RequestPath(new PathRequest(new Vector2Int(0, 0), new Vector2Int(Random.Range(0, gridManager.GetGrid().Width), Random.Range(0, gridManager.GetGrid().Width)), pathRequestCallback));
            }
        }

        private void GetPathResult(Queue<PathNode> path, bool success)
        {
            requestedPath = false;
            if (success) { curPath = path; }
            else { Debug.Log("Couldn't find a path properly."); }
        }


        private void MoveTo(PathNode target)
        {
            var position = transform.position;
            var targetDestination = new Vector3(target.GetX(), position.y, target.GetY());
            var destination = Vector3.MoveTowards(position, targetDestination, maxMoveSpeed);
            transform.position = destination;
            
            if (Vector3.Distance(destination, targetDestination) < 0.05f)
            {
                transform.position = targetDestination;
                curTarget = null;
            }
        }
    }
}
