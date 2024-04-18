using UnityEngine;

namespace CodeBase.Game.DrawGizmo
{
    public sealed class DrawSphere : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField, Range(0f, 5f)] private float _radius;

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}