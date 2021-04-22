using System;
using SelectObjects;
using UnityEngine;


/// <summary>
/// This class doesn't implement any I<Event>Handler interfaces and work directly with raycasts and Input.
/// This prevents other objects besides selection plane to get hit by raycasting
/// </summary>
public class SelectWithMouseViaRaycast : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectionFrameBase _selectionFrame;
    
    private bool _isDragging;

    private Vector3 _startDragPosition;
    private Vector3 _endDragPosition;
    private Rect _selectedRect;

    private RaycastHit[] _raycastHitsBuffer = new RaycastHit[1];

    private int _layerMask;

    private ISelectionCoordinatesConverter _selectionCoordinatesConverter;

    private void Awake()
    {
        _layerMask = LayerMask.GetMask("SelectionPlane");
        _selectionCoordinatesConverter = new LocalSpaceSelectionCoordinatesConverter(transform);
    }
    
    private void Update()
    {
        if (!Input.GetMouseButton(0) && !_isDragging)
        {
            _selectionFrame.SetVisibility(false);
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            if (!GetRaycastHitPosition(out var raycastHitPosition))
            {
                return;
            }
            
            if (!_isDragging)
            {
                _isDragging = true;
                _startDragPosition = raycastHitPosition;
                _selectionFrame.SetVisibility(true);
            }
            else
            {
                _endDragPosition = raycastHitPosition;
                UpdateSelectedRect();
                _selectionFrame.UpdateRect(_selectedRect);
            }
        }
        else if (_isDragging)
        {
            if (!GetRaycastHitPosition(out var raycastHitPosition))
            {
                return;
            }

            _endDragPosition = raycastHitPosition;
            _isDragging = false;
            UpdateSelectedRect();
            _selectionFrame.UpdateRect(_selectedRect);
        }
        else
        {
            _selectionFrame.SetVisibility(false);
        }
    }

    private bool GetRaycastHitPosition(out Vector3 raycastHitPosition)
    {
        var mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        var size = Physics.RaycastNonAlloc(ray, _raycastHitsBuffer, float.MaxValue, _layerMask);
        if (size == 0)
        {
            raycastHitPosition = Vector3.zero;
            return false;
        }
                
        var worldSpaceHitPoint = _raycastHitsBuffer[0].point;
        var localSpaceHitPoint = _raycastHitsBuffer[0].transform.InverseTransformPoint(worldSpaceHitPoint);
        raycastHitPosition = localSpaceHitPoint;
        return true;
    }

    private void UpdateSelectedRect()
    {
        _selectedRect = _selectionCoordinatesConverter.GetSelectionRect(_startDragPosition, _endDragPosition);

        Camera camera = Camera.main;

        foreach (var selectable in Selectables.SelectableObjects)
        {
            bool selected = selectable.GetSelectableChecker().CheckSelected(camera, _selectedRect, transform, _selectionCoordinatesConverter);
            selectable.SetSelected(selected);
        }
    }
}
