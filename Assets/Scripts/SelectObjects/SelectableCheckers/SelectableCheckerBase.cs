using UnityEngine;

namespace SelectObjects.SelectableCheckers
{
    public abstract class SelectableCheckerBase : MonoBehaviour, ISelectableChecker
    {
        public abstract bool CheckSelected(Camera raycastCamera, Rect screenSelectionRect, Transform rectSpaceTransform);
    }
}