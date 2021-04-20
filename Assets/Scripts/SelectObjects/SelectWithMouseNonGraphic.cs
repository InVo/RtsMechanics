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
    private ISelectionCoordinatesConverter _selectionCoordinatesConverter;

    private void Awake()
    {
        _selectionCoordinatesConverter = new LocalSpaceSelectionCoordinatesConverter(transform);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _selectedRect = Rect.zero;
        _selectionFrame.SetVisibility(true);
        _selectionFrame.UpdateRect(_selectedRect);
        _startDragPosition = _selectionCoordinatesConverter.GetPositionFromPointerEventData(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _endDragPosition = _selectionCoordinatesConverter.GetPositionFromPointerEventData(eventData);
        UpdateSelectedRect();
        _selectionFrame.SetVisibility(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _endDragPosition = _selectionCoordinatesConverter.GetPositionFromPointerEventData(eventData);
        UpdateSelectedRect();
        _selectionFrame.UpdateRect(_selectedRect);
    }

    private void UpdateSelectedRect()
    {
        _selectedRect = _selectionCoordinatesConverter.GetSelectionRect(_startDragPosition, _endDragPosition);
        
        FillSelectablesList();

        Camera camera = Camera.main;

        foreach (var selectable in _selectables)
        {
            bool selected = selectable.GetSelectableChecker().CheckSelected(camera, _selectedRect, transform, _selectionCoordinatesConverter);
            selectable.SetSelected(selected);
        }
    }

    private void FillSelectablesList()
    {
        _selectables = Selectables.SelectableObjects;
    }
}
