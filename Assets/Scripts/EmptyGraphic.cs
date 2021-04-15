using UnityEngine.UI;

public class EmptyGraphic : Graphic
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }

    public override void SetAllDirty()
    { }

    public override void SetLayoutDirty()
    { }

    public override void SetMaterialDirty()
    { }
}
