using UnityEngine;

public class PlayerController : CharacterController
{
    // Dependencies
    [SerializeField] private InputManager inputManager;

    protected override void Start()
    {
        base.Start();
        
        inputManager.PlayerMovedFixed += OnPlayerMovedFixed;
    }
    
    private void OnPlayerMovedFixed(Vector2 movement)
    {
        Move(movement);
    }
}