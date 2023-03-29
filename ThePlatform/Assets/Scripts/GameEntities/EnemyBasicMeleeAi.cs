using System;
using System.Linq;
using Pathfinding;
using UnityEngine;

namespace GameEntities
{
    [RequireComponent(typeof(EnemyController))]
    public class EnemyBasicMeleeAi : EnemyAi
    {
        private TilemapPathfinding _pathfinding;
        private EnemyController _controller;
        
        private void Start()
        {
            _pathfinding = FindObjectOfType<TilemapPathfinding>();
            _controller = GetComponent<EnemyController>();
        }

        public void FixedUpdate()
        {
            base.OnUpdate(_controller);
            
            var player = FindObjectOfType<PlayerCharacter>();

            var path = _pathfinding.GetPath(
                Vector2Int.RoundToInt(transform.position),
                Vector2Int.RoundToInt(player.transform.position));

            var pathArray = path as Vector2[] ?? path.ToArray();
            for (int i = 0; i < pathArray.Count() - 1; i++)
            {
                Debug.DrawLine(pathArray[i], pathArray[i + 1], Color.red);
            }
            
            if(!pathArray.Any())
                return;

            var target = pathArray.First();
            var direction = (target - (Vector2)transform.position).normalized;
            
            Debug.DrawLine((Vector2)transform.position, target, Color.magenta);
            
            _controller.Move(direction);
        }
        
        
    }
}