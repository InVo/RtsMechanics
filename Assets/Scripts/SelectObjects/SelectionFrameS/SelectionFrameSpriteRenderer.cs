using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionFrameSpriteRenderer : SelectionFrameBase
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public override void UpdateRect(Rect localSpaceRect)
    {
        _spriteRenderer.size = localSpaceRect.size;
        _spriteRenderer.transform.localPosition = new Vector3(localSpaceRect.center.x, transform.localPosition.y, localSpaceRect.center.y);
    }

    public override void SetVisibility(bool visible)
    {
        _spriteRenderer.enabled = visible;
    }
}
