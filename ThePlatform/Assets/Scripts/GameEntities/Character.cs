using UnityEngine;

namespace GameEntities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : GameEntity
    {
        // Dependencies
        [HideInInspector] private Rigidbody2D _rigidbody2D;
        
        // Properties
        public Vector3 Velocity
        {
            get => _rigidbody2D.velocity;
            set => _rigidbody2D.velocity = value;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}