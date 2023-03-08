using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pathfinding
{

    public class TilemapPathfinding : MonoBehaviour
    {
        // Dependencies
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Tilemap wallTileMap;
    
        // Settings
        [SerializeField] private bool showDebug = true;
        
        // Private fields and properties
        private AStar.NodeMap<TileBase> _nodeMap; 
        
        // Const values
        private const int MaxPath = 75;
        
        private void Start()
        {
            _nodeMap = ConstructNodeMap(tilemap);

            Debug.Log($"Calculated {_nodeMap.Map.Count} {nameof(AStar.NavNode)}s");
        }

        private void Update()
        {
            if (showDebug)
            {
                foreach (var node in _nodeMap.Map)
                {
                    var position = node.Key.Position;
                    var neighbours = node.Key.ActiveNeighbours;

                    foreach (var neighbour in neighbours)
                    {
                        Debug.DrawLine(position, neighbour.Position);
                    }
                }
            }
        }
        
        public IEnumerable<Vector2> GetPath(Vector2 start, Vector2 goal)
        {
            return AStar.PathFindingNodes(start, goal, _nodeMap, MaxPath)
                .Select(x => x.Position);
        }
        
        private static AStar.NodeMap<TileBase> ConstructNodeMap(Tilemap tilemap)
        {            
            var nodeMap = new AStar.NodeMap<TileBase>();

            var bounds = tilemap.cellBounds;
            foreach (var tilePosition in bounds.allPositionsWithin)
            {
                var tile = tilemap.GetTile(tilePosition);
                
                if(tile is null)
                    continue;
                
                nodeMap.AddNode(new AStar.NavNode
                {
                    Position = (Vector2Int)tilePosition
                }, tile);
            }

            nodeMap.CalculateDistances();
            nodeMap.CreateEdgesUsingDistances(1.5f);

            return nodeMap;
        }
    }
}
