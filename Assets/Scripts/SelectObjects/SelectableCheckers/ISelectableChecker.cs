using UnityEngine;

namespace SelectObjects.SelectableCheckers
{
    public interface ISelectableChecker
    {
        bool CheckSelected(Camera raycastCamera, Rect screenSelectionRect, Transform rectSpaceTransform, ISelectionCoordinatesConverter coordsConverter);
    }
}