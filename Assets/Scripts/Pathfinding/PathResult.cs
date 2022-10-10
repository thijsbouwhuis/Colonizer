using System.Collections.Generic;

namespace Pathfinding
{
    struct PathResult
    {
        private Queue<PathNode> path;
        private PathRequest pathRequest;
        private bool success;
        
        public Queue<PathNode> Path { get => path; }
        public PathRequest PathRequest { get => pathRequest; }
        public bool Success { get => success; }

        public PathResult(Queue<PathNode> path, PathRequest pathRequest, bool success)
        {
            this.path = path;
            this.pathRequest = pathRequest;
            this.success = success;
        }
    }
}