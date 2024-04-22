using UnityEngine;

namespace CodeBase.Game.DrawGizmo
{
    public sealed class DrawSphere : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.red;
        [SerializeField, Range(0f, 5f)] private float _radius = 0.5f;

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}