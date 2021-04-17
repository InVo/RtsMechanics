using UnityEngine;

namespace SelectObjects.SelectableCheckers
{
    public class BoxColliderSelectableChecker : SelectableCheckerBase
    {
        [SerializeField] 
        private BoxCollider _boxCollider;
        
        public override bool CheckSelected(Camera raycastCamera, Rect selectionRect, Transform rectSpaceTransform)
        {
            var vertices = new Vector3[8];
            var halfSize = _boxCollider.size / 2f;
            var center = _boxCollider.center;
            vertices[0] = center + new Vector3(halfSize.x, halfSize.y, halfSize.z);
            vertices[1] = center + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
            vertices[2] = center + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
            vertices[3] = center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
            vertices[4] = center + new Vector3(-halfSize.x, halfSize.y, halfSize.z);
            vertices[5] = center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
            vertices[6] = center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
            vertices[7] = center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
 
            foreach (var vertice in vertices)
            {
                Vector3 worldSpaceVertice = transform.TransformPoint(vertice);
                //Vector2 localSpaceVertice = raycastCamera.WorldToScreenPoint(worldSpaceVertice);
                Vector3 localSpaceVertice = rectSpaceTransform.InverseTransformPoint(worldSpaceVertice);
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
