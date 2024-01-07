using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CMove : EntityComponent<CMove>
    {
        [SerializeField] private CharacterController _characterController;
        
        public CharacterController CharacterController => _characterController;
        public Vector3 Position => transform.position;
        public float Angle => transform.eulerAngles.y;
        public float BaseSpeed { get; set; }
        public float Speed { get; set; }
        public bool IsGrounded => _characterController.isGrounded;
    }
}