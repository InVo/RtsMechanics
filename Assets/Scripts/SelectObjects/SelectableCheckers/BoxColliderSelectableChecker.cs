using UnityEngine;

namespace SelectObjects.SelectableCheckers
{
    public class BoxColliderSelectableChecker : SelectableCheckerBase
    {
        [SerializeField] 
        private BoxCollider _boxCollider;

        private Vector3[] _vertices = new Vector3[8];
        
        private void Awake()
        {
            var halfSize = _boxCollider.size / 2f;
            var center = _boxCollider.center;
            _vertices[0] = center + new Vector3(halfSize.x, halfSize.y, halfSize.z);
            _vertices[1] = center + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
            _vertices[2] = center + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
            _vertices[3] = center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
            _vertices[4] = center + new Vector3(-halfSize.x, halfSize.y, halfSize.z);
            _vertices[5] = center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
            _vertices[6] = center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
            _vertices[7] = center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
        }
        
        public override bool CheckSelected(Camera raycastCamera, Rect selectionRect, Transform rectSpaceTransform, 
            ISelectionCoordinatesConverter coordsConverter)
        {
            foreach (var vertice in _vertices)
            {
                Vector3 worldSpaceVertice = transform.TransformPoint(vertice);
                Vector3 localSpaceVertice = coordsConverter.WorldSpaceVerticeToSelectionSpace(worldSpaceVertice);
                if (localSpaceVertice.x < selectionRect.xMin || 
                    localSpaceVertice.x > selectionRect.xMax ||
                    localSpaceVertice.y < selectionRect.yMin ||
                    localSpaceVertice.y > selectionRect.yMax)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
