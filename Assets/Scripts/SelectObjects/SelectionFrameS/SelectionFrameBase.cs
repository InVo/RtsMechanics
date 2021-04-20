using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectionFrameBase : MonoBehaviour
{
    public abstract void UpdateRect(Rect rect);
    public abstract void SetVisibility(bool visible);
}
