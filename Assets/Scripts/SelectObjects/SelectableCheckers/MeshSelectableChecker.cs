using UnityEngine;

namespace SelectObjects.SelectableCheckers
{
    public class MeshSelectableChecker : SelectableCheckerBase
    {
        [SerializeField] 
        private MeshCollider _meshCollider;

        private Vector3[] _vertices;
        
        public override bool CheckSelected(Camera raycastCamera, Rect screenSelectionRect, Transform rectSpaceTransform, 
            ISelectionCoordinatesConverter selectionCoordinatesConverter)
        {
            _vertices = _meshCollider.sharedMesh.vertices;
            foreach (var vertice in _vertices)
            {
                Vector3 worldSpaceVertice = transform.TransformPoint(vertice);
                Vector2 selectionSpaceVertice = selectionCoordinatesConverter.WorldSpaceVerticeToSelectionSpace(worldSpaceVertice);
                if (selectionSpaceVertice.x < screenSelectionRect.xMin || 
                    selectionSpaceVertice.x > screenSelectionRect.xMax ||
                    selectionSpaceVertice.y < screenSelectionRect.yMin ||
                    selectionSpaceVertice.y > screenSelectionRect.yMax)
                {
                    return false;
                }
            }

            return true;
        }
    }
}