using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pathfinding
{
    public class AStar
    {
        public static List<T> PathFinding<T>(T start, T goal, NodeMap<T> nodeMap, int breakPoint = 35) where T : class
        {
            var startNode = nodeMap.Map.FirstOrDefault(x => x.Value == start).Key;
            var goalNode = nodeMap.Map.FirstOrDefault(x => x.Value == goal).Key;

            var nodePath = AStarAlgorithm(
                startNode,
                goalNode,
                nodeMap.Nodes,
                breakPoint);

            if (nodePath == null)
                return null;

            return nodePath.Select(node => nodeMap.Map[node]).ToList();
        }
        
        public static List<NavNode> PathFindingNodes<T>(Vector2 start, Vector2 goal, NodeMap<T> nodeMap, int breakPoint = 35) where T : class
        {
            var startNode = nodeMap.NodePositions[start];
            var goalNode = nodeMap.NodePositions[goal];


            var nodePath = AStarAlgorithm(
                startNode,
                goalNode,
                nodeMap.Nodes,
                breakPoint);

            if (nodePath == null)
                return null;

            return nodePath;
        }
        
        static List<NavNode> ReconstructPath(Dictionary<NavNode, NavNode> cameFrom, NavNode currentNavNode)
        {
            if (cameFrom.ContainsKey(currentNavNode))
            {
                List<NavNode> p = ReconstructPath(cameFrom, cameFrom[currentNavNode]);
                p.Add(currentNavNode);                                            // p + current_node ????
                return p;
            }
            else
                return new List<NavNode>();
        }

        private static List<NavNode> AStarAlgorithm(NavNode start, NavNode goal, List<NavNode> allNodes, int breakPoint)
        {
            var cameFrom = new Dictionary<NavNode, NavNode>();

            var closedSet = new List<NavNode>();            // Searched through nodes                  
            var openSet = new List<NavNode> { start };      // Nodes to be searched
            var gScore = new Dictionary<NavNode, float>();  // Optimal path length adjusted for costs
            var hScore = new Dictionary<NavNode, float>();  // Assumed path length
            var fScore = new Dictionary<NavNode, float>();  // Combination of g and h score
            gScore.Add(start, 0);

            foreach (NavNode node in allNodes)
            {
                //g_score.Add(node, 0f);
                fScore.Add(node, float.MaxValue);
            }

            int nodesChecked = 0;

            while (openSet.Count > 0)
            {  //  while open set is not empty 
                var temp = openSet.OrderBy(n => fScore[n]).ToList();
                var x = temp.First();

                if (x == goal)
                {
                    return ReconstructPath(cameFrom, goal);
                }

                openSet.Remove(x);
                closedSet.Add(x);

                foreach (NavNode y in x.ActiveNeighbours.OrderBy(_x => _x.DistanceTo(goal)))
                {
                    if (closedSet.Contains(y))
                        continue;
                    float tentativeGScore = gScore[x] + x.DistanceTo(y);

                    bool tentativeIsBetter = false;
                    if (!openSet.Contains(y))
                    {
                        openSet.Add(y);
                        hScore.Add(y, y.DistanceTo(goal));
                        tentativeIsBetter = true;
                    }
                    else if (tentativeGScore < gScore[y])
                    {
                        tentativeIsBetter = true;
                    }
                    if (tentativeIsBetter == true)
                    {
                        if (cameFrom.ContainsKey(y))
                            cameFrom[y] = x;
                        else
                            cameFrom.Add(y, x);
                        gScore[y] = tentativeGScore;

                        fScore[y] = gScore[y] + hScore[y];
                    }
                }
                nodesChecked++;
                if (nodesChecked > breakPoint)
                    return null;
            }
            return null;
        }
        public class NodeMap<T>
        {
            public List<NavNode> Nodes { get; } = new();
            public Dictionary<NavNode, T> Map { get; } = new();
            public Dictionary<Vector2, NavNode> NodePositions { get; } = new();


            public void CreateEdgesUsingDistances(float maxDistance)
            {
                for (int i = 0; i < Nodes.Count; i++)
                {
                    for (int j = i + 1; j < Nodes.Count; j++)
                    {
                        if (Nodes[i].Distances[Nodes[j]] <= maxDistance)
                        {
                            Nodes[i].Neighbours.Add(Nodes[j]);
                            Nodes[j].Neighbours.Add(Nodes[i]);
                        }
                    }
                }
            }
            public void CalculateDistances()
            {
                for (int i = 0; i < Nodes.Count; i++)
                {
                    Nodes[i].Distances.Add(Nodes[i], 0);
                    for (int j = i + 1; j < Nodes.Count; j++)
                    {
                        float dis = Vector2.Distance(Nodes[i].Position, Nodes[j].Position);
                        Nodes[i].Distances.Add(Nodes[j], dis);
                        Nodes[j].Distances.Add(Nodes[i], dis);
                    }
                }
            }

            public void AddNode(NavNode navNode, T t)
            {
                Nodes.Add(navNode);
                Map.Add(navNode, t);
                NodePositions.Add(navNode.Position, navNode);
            }
        }
        public class NavNode
        {
            public IEnumerable<NavNode> ActiveNeighbours { get => Neighbours.Where(x => x.IsActiveCheck()); }
            public List<NavNode> Neighbours { get; set; } = new List<NavNode>();
            public Dictionary<NavNode, float> Distances { get; set; } = new Dictionary<NavNode, float>();
            public float Cost { get; set; }
            public Vector2 Position { get; set; }
            
            public delegate bool IsActiveCheckDelegate();

            public IsActiveCheckDelegate IsActiveCheck { get; } = () => true;

            public float DistanceTo(NavNode navNode)
            {
                if (Distances.TryGetValue(navNode, out float distance))
                    return distance;
                
                Distances.Add(navNode, Vector2.Distance(Position, navNode.Position));
                return Distances[navNode];
            }
        }
    }
}
