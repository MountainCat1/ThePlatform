using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    // Dependencies
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CharacterAnimator characterAnimator;
    
    [HideInInspector] private Rigidbody2D _rigidbody2D;
    // Settings
    [SerializeField] private float speed = 1f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        inputManager.PlayerMovedFixed += OnPlayerMovedFixed;
        inputManager.PlayerNotMovedFixed += OnPlayerNotMovedFixed;
    }

    private void OnPlayerNotMovedFixed()
    {
        characterAnimator.Play(CharacterAnimator.Animation.Idle);

        _rigidbody2D.velocity = Vector2.zero;
    }

    private void OnPlayerMovedFixed(Vector2 move)
    {
        _rigidbody2D.velocity = move * speed;
        
        characterAnimator.Play(CharacterAnimator.Animation.Walk);

        characterAnimator.Flipped = move.x < 0;
    }
}
