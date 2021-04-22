using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenSpaceSelectionCoordinatesConverter : ISelectionCoordinatesConverter
{
    private Camera _raycastCamera;
    
    public ScreenSpaceSelectionCoordinatesConverter(Camera raycastCamera)
    {
        _raycastCamera = raycastCamera;
    }

    public Vector3 GetPositionFromPointerEventData(PointerEventData eventData)
    {
        return eventData.position;
    }

    public Rect GetSelectionRect(Vector3 startPosition, Vector3 endPosition)
    {
        Vector2 position = new Vector2(Math.Min(endPosition.x, startPosition.x),
            Math.Min(endPosition.y, startPosition.y));
        Vector2 size = new Vector2(Math.Abs(endPosition.x - startPosition.x), Math.Abs(endPosition.y - startPosition.y));
        return new Rect(position, size);
    }

    public Vector3 WorldSpaceVerticeToSelectionSpace(Vector3 vertice)
    {
        return _raycastCamera.WorldToScreenPoint(vertice);
    }
}
