using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameEntities;
using Pathfinding;
using UnityEngine;

public class Enemy : Character
{
    // Dependencies
    [SerializeField] private EnemyAi ai;
    [SerializeField] private EnemyController controller;

    
    //
    private void Start()
    {

    }
}
