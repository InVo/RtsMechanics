using SelectObjects;
using UnityEngine;

public class SelectableViaMaterial : SelectableBase
{
    [SerializeField] private MeshRenderer _meshRenderer;
    
    [Header("Materials")]
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private Material _unselectedMaterial;
    
    public override void SetSelected(bool value)
    {
        _meshRenderer.material = value ? _selectedMaterial : _unselectedMaterial;
    }
}
