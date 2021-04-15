using System.Collections;
using System.Collections.Generic;
using SelectObjects;
using SelectObjects.SelectableCheckers;
using UnityEngine;

namespace SelectObjects
{
    public interface ISelectable
    {
        void SetSelected(bool value);
    }

//Class is required to make it work with FindObjectsOfType
    public abstract class SelectableBase : MonoBehaviour, ISelectable
    {
        [SerializeField] 
        private SelectableCheckerBase _selectableChecker;
        
        public abstract void SetSelected(bool value);

        private void OnEnable()
        {
            Selectables.AddSelectable(this);
        }

        private void OnDisable()
        {
            Selectables.RemoveSelectable(this);
        }

        public ISelectableChecker GetSelectableChecker()
        {
            return _selectableChecker;
        }
    }
}


