using UnityEngine;
using UnityEngine.EventSystems;

public interface ISelectionCoordinatesConverter
{
    Vector3 GetPositionFromPointerEventData(PointerEventData eventData);
    Rect GetSelectionRect(Vector3 startPosition, Vector3 endPosition);
    Vector3 WorldSpaceVerticeToSelectionSpace(Vector3 vertice);
}