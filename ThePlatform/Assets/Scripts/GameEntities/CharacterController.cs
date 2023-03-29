using System;
using System.Collections;
using System.Collections.Generic;
using GameEntities;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [Header("Dependencies")] [SerializeField]
    private CharacterAnimator spriteAnimator;

    [SerializeField] private Rigidbody2D rb;

    [Header("Settings")]
    // Settings
    public float moveSpeed = 200f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        var movement = direction.normalized * (moveSpeed * Time.deltaTime);
        
        rb.AddForce(movement, ForceMode2D.Impulse);

        if (spriteAnimator is not null)
        {
            if (movement.x != 0)
                spriteAnimator.Flipped = movement.x < 0;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (spriteAnimator is not null)
        {
            if (rb.velocity.magnitude > 0.25f)
                spriteAnimator.PlayAnimation(CharacterAnimator.Animation.Walk);
            else
                spriteAnimator.PlayAnimation(CharacterAnimator.Animation.Idle);
        }
    }
}