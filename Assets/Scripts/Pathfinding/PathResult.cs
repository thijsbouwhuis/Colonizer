using System.Collections.Generic;

namespace Pathfinding
{
    struct PathResult
    {
        private List<PathNode> path;
        private PathRequest pathRequest;
        private bool success;
        
        public List<PathNode> Path { get => path; }
        public PathRequest PathRequest { get => pathRequest; }
        public bool Success { get => success; }

        public PathResult(List<PathNode> path, PathRequest pathRequest, bool success)
        {
            this.path = path;
            this.pathRequest = pathRequest;
            this.success = success;
        }
    }
}