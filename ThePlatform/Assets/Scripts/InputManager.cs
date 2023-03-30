using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    // Events

    public event Action<Vector2> PlayerMovedFixed;
    public event Action PlayerNotMovedFixed;

    public event Action<Vector2> PlayerMoved;
    public event Action PlayerNotMoved;

    // Private fields

    private InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Enable();
    }

    private void Update()
    {
        var move = _inputActions.Player.Move.ReadValue<Vector2>();
        if (move.magnitude > 0)
            PlayerMoved?.Invoke(move);
        else
            PlayerNotMoved?.Invoke();
    }

    private void FixedUpdate()
    {
        var move = _inputActions.Player.Move.ReadValue<Vector2>();
        if (move.magnitude > 0)
            PlayerMovedFixed?.Invoke(move);
        else
            PlayerNotMovedFixed?.Invoke();
    }
}