using UnityEngine;
using UnityEngine.UI;

namespace SelectObjects
{
    public class SelectionFrameImage : SelectionFrameBase
    {
        [SerializeField] private Image _rectImage;

        private RectTransform _rectTransform;
        
        private void Awake()
        {
            _rectTransform = _rectImage.rectTransform;
            //counting coordinates from Canvas zero-zero
            _rectTransform.pivot = Vector2.zero;
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.zero;
        }
        
        public override void UpdateRect(Rect screenSpaceRect)
        {
            _rectTransform.anchoredPosition = screenSpaceRect.position;
            _rectTransform.sizeDelta = screenSpaceRect.size;
        }

        public override void SetVisibility(bool visible)
        {
            _rectImage.enabled = visible;
        }
    }
}