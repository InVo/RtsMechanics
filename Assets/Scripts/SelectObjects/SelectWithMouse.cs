using System;
using System.Collections.Generic;
using SelectObjects;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectWithMouse : EmptyGraphic, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] 
    private SelectionFrameBase _selectionFrame;
    
    private Vector2 _startDragPosition;
    private Vector2 _endDragPosition;
    private Rect _selectedRect;
    private List<SelectableBase> _selectables;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _selectedRect = Rect.zero;
        _selectionFrame.SetVisibility(true);
        _selectionFrame.UpdateRect(_selectedRect);
        _startDragPosition = eventData.position;
        _startDragPosition = transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _endDragPosition = eventData.position;
        _endDragPosition = transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
        UpdateSelectedRect();
        _selectionFrame.SetVisibility(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _endDragPosition = eventData.position;
        _endDragPosition = transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
        UpdateSelectedRect();
        _selectionFrame.UpdateRect(_selectedRect);
    }

    private void UpdateSelectedRect()
    {
        Vector2 center = new Vector2(Math.Min(_endDragPosition.x, _startDragPosition.x),
            Math.Min(_endDragPosition.y, _startDragPosition.y));
        Vector2 size = new Vector2(Math.Abs(_endDragPosition.x - _startDragPosition.x), Math.Abs(_endDragPosition.y - _startDragPosition.y));
        _selectedRect = new Rect(center, size);
        
        FillSelectablesList();

        Camera camera = Camera.main;

        foreach (var selectable in _selectables)
        {
            bool selected = selectable.GetSelectableChecker().CheckSelected(camera, _selectedRect, transform);
            selectable.SetSelected(selected);
        }
    }

    private void FillSelectablesList()
    {
        _selectables = Selectables.SelectableObjects;
    }
}
