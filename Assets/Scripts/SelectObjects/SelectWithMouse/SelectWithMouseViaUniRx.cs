using SelectObjects;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class SelectWithMouseViaUniRx : EmptyGraphic
{
    [SerializeField] private SelectionFrameBase _selectionFrame;
    private Vector3 _beginDragPosition;
    private Vector3 _endDragPosition;
    private Rect _selectedRect;

    private ISelectionCoordinatesConverter _selectionCoordinatesConverter;
    
    private void Awake()
    {
        _selectionCoordinatesConverter = new ScreenSpaceSelectionCoordinatesConverter(Camera.main);
    }
    
    private void Start()
    {
        this.OnBeginDragAsObservable()
            .Select(_ => Input.mousePosition)
            .RepeatUntilDestroy(this)
            .Subscribe(position =>
            {
                _beginDragPosition = position;
                _selectedRect = Rect.zero;
                _selectionFrame.UpdateRect(_selectedRect);
            });
        
        this.OnDragAsObservable()
            .TakeUntil(this.OnEndDragAsObservable())
            .Select(_ => Input.mousePosition)
            .RepeatUntilDestroy(this)
            .Subscribe((unit =>
            {
                _endDragPosition = unit;
                UpdateSelectedRect();
                _selectionFrame.UpdateRect(_selectedRect);
            }), () => { });

        this.OnEndDragAsObservable()
            .Select(_ => Input.mousePosition)
            .RepeatUntilDestroy(this)
            .Subscribe(position =>
            {
                _selectedRect = Rect.zero;
                _selectionFrame.UpdateRect(_selectedRect);
            });
    }
    
    private void UpdateSelectedRect()
    {
        _selectedRect = _selectionCoordinatesConverter.GetSelectionRect(_beginDragPosition, _endDragPosition);
        Camera camera = Camera.main;

        foreach (var selectable in Selectables.SelectableObjects)
        {
            bool selected = selectable.GetSelectableChecker().CheckSelected(camera, _selectedRect, transform, _selectionCoordinatesConverter);
            selectable.SetSelected(selected);
        }
    }
}
