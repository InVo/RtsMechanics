using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocalSpaceSelectionCoordinatesConverter : ISelectionCoordinatesConverter
{
    private readonly Transform _transform;
    
    public LocalSpaceSelectionCoordinatesConverter(Transform transform)
    {
        _transform = transform;
    }
    
    public Vector3 GetPositionFromPointerEventData(PointerEventData eventData)
    {
        return _transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
    }

    public Rect GetSelectionRect(Vector3 startPosition, Vector3 endPosition)
    {
        Vector2 position = new Vector2(Math.Min(endPosition.x, startPosition.x),
            Math.Min(endPosition.z, startPosition.z));
        Vector2 size = new Vector2(Math.Abs(endPosition.x - startPosition.x), Math.Abs(endPosition.z - startPosition.z));
        return new Rect(position, size);
    }

    public Vector3 WorldSpaceVerticeToSelectionSpace(Vector3 vertice)
    {
        throw new System.NotImplementedException();
    }
}
