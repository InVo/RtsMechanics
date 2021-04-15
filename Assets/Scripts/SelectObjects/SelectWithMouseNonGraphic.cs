using System;
using System.Collections.Generic;
using SelectObjects;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectWithMouseNonGraphic : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] 
    private SelectionFrameBase _selectionFrame;
    
    private Vector3 _startDragPosition;
    private Vector3 _endDragPosition;
    private Rect _selectedRect;
    private List<SelectableBase> _selectables;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _selectedRect = Rect.zero;
        _selectionFrame.SetVisibility(true);
        _selectionFrame.UpdateRect(_selectedRect);
        _startDragPosition = transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _endDragPosition = transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
        UpdateSelectedRect();
        _selectionFrame.SetVisibility(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _endDragPosition = transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
        UpdateSelectedRect();
        _selectionFrame.UpdateRect(_selectedRect);
    }

    private void UpdateSelectedRect()
    {
        Vector2 center = new Vector2(Math.Min(_endDragPosition.x, _startDragPosition.x),
            Math.Min(_endDragPosition.z, _startDragPosition.z));
        Vector2 size = new Vector2(Math.Abs(_endDragPosition.x - _startDragPosition.x), Math.Abs(_endDragPosition.z - _startDragPosition.z));
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
