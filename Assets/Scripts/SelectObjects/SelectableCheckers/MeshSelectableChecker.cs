using UnityEngine;

namespace SelectObjects.SelectableCheckers
{
    public class MeshSelectableChecker : SelectableCheckerBase
    {
        [SerializeField] 
        private MeshCollider _meshCollider;

        private Vector3[] _vertices;
        
        public override bool CheckSelected(Camera raycastCamera, Rect screenSelectionRect, Transform rectSpaceTransform)
        {
            _vertices = _meshCollider.sharedMesh.vertices;
            foreach (var vertice in _vertices)
            {
                Vector3 worldSpaceVertice = transform.TransformPoint(vertice);
                Vector2 localSpaceVertice = rectSpaceTransform.InverseTransformPoint(worldSpaceVertice);
                if (localSpaceVertice.x < screenSelectionRect.xMin || 
                    localSpaceVertice.x > screenSelectionRect.xMax ||
                    localSpaceVertice.y < screenSelectionRect.yMin ||
                    localSpaceVertice.y > screenSelectionRect.yMax)
                {
                    return false;
                }
            }

            return true;
        }
    }
}