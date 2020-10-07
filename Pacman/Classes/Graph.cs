using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Classes
{
    class Graph
    {
        private Stack<Vertex> _current;
        private int limit;
        public bool _goingBack = false;

        public Graph(List<int> directions, int startLimit)
        {
            _current = new Stack<Vertex>();
            Vertex startVertex = new Vertex(directions);
            _current.Push(startVertex);
            limit = startLimit;
        }

        public int GoBack()
        {
            if (_current.Count == 1)
            {
                return 0;
            }
            Vertex leaveVertex = _current.Pop();
            leaveVertex.ResetVisited();
            return leaveVertex._cameFrom;
        }

        public int Go(int currentDir, List<int> observe)
        {
            if(_current.Count == 1 && !_current.Peek().CheckIfSomeNotVisited())
            {
                _current.Peek().ResetVisited();
                ++limit;
            }
            if(_current.Count < limit + 1 && !_goingBack)
            {
                SaveNewKnowledge(observe);
            }
            if (_current.Peek().CheckIfSomeNotVisited())
            {
                KeyValuePair<Vertex, int> toVertex = _current.Peek().NotVisited();
                _current.Push(toVertex.Key);
                _goingBack = false;
                return toVertex.Value;
            }
            if(_current.Peek()._nextVertices.Count == 0)
            {
                _current.Peek()._cameFrom = Opposite(currentDir);
            }
            _goingBack = true;
            return GoBack();
        }

        private bool SaveNewKnowledge(List<int> newPaths)
        {
            if (_current.Peek().NoNewPaths())
            {
                _current.Peek().FillNewKnowledge(newPaths);

                return true;
            }
            return false;
        }


        public int Opposite(int cur)
        {
            if (cur < 3)
            {
                return cur + 2;
            }
            return cur == 3 ? 1 : 2;
        }
    }
}
