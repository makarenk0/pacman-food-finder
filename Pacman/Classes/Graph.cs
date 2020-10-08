using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Pacman.Classes
{
    class Graph
    {
        private Stack<Vertex> _current;
        private int limit;
        public bool _goingBack = false;

        private int memoryUsage = 0;
        private int stepsAmount = 0;
        Stopwatch stopWatch;

        public Graph(List<short> directions, int startLimit)
        {
            _current = new Stack<Vertex>();
            Vertex startVertex = new Vertex(directions);
            _current.Push(startVertex);
            limit = startLimit;

            stopWatch = new Stopwatch();
            stopWatch.Start();
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

        public int Go(short currentDir, List<short> observe)   //Iterative deepening depth-first search
        {
            PrintDetails();
            if (_current.Peek()._nextVertices.Count == 0)
            {
                _current.Peek()._cameFrom = Opposite(currentDir);
            }

            if (_current.Count == 1 && !_current.Peek().CheckIfSomeNotVisited())
            {
                _current.Peek().ResetVisited();
                ++limit;
            }
            if(_current.Count < limit && !_goingBack)
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
            
            _goingBack = true;
            return GoBack();
        }

        private bool SaveNewKnowledge(List<short> newPaths)
        {
            if (_current.Peek().NoNewPaths())
            {
                _current.Peek().FillNewKnowledge(newPaths);

                memoryUsage += _current.Peek().GetMemoryAmount();

                return true;
            }
            return false;
        }


        public short Opposite(short cur)
        {
            if (cur < 3)
            {
                return (short)(cur + 2);
            }
            return (short)(cur == 3 ? 1 : 2);
        }

        private void PrintDetails()
        {
            ++stepsAmount;
            Console.Clear();
            Console.WriteLine(String.Concat("Memory used: ", memoryUsage, " bytes"));
            Console.WriteLine(String.Concat("Steps done: ", stepsAmount));
            Console.WriteLine(String.Concat("Elapsed time: ", stopWatch.Elapsed, " hh:mm:ss: millis"));
        }
    }
}
