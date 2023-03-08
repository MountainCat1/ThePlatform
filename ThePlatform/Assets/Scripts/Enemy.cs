using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private TilemapPathfinding _pathfinding;
    
    private void Start()
    {
        _pathfinding = FindObjectOfType<TilemapPathfinding>();
    }

    private void Update()
    {
        var player = FindObjectOfType<CharacterController>();

        var path = _pathfinding.GetPath(
            Vector2Int.RoundToInt(transform.position),
            Vector2Int.RoundToInt(player.transform.position));

        var pathArray = path as Vector2[] ?? path.ToArray();
        for (int i = 0; i < pathArray.Count() - 1; i++)
        {
            Debug.DrawLine(pathArray[i], pathArray[i + 1]);
        }
    }
}
