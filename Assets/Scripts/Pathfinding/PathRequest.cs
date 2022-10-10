using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class PathRequest {
        private Vector2Int start;
        private Vector2Int end;
        private Action<List<PathNode>, bool> callback;

        public Vector2Int Start { get => start;}
        public Vector2Int End { get => end;}
        public Action<List<PathNode>, bool> Callback { get => callback; }

        public PathRequest(Vector2Int start, Vector2Int end, Action<List<PathNode>, bool> callback)
        {
            this.start = start;
            this.end = end;
            this.callback = callback;
        }
    }
}