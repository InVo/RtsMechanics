using System.Collections.Generic;

namespace SelectObjects
{
    /// <summary>
    /// Collection of objects which can be selected via game logic
    /// </summary>
    public static class Selectables
    {
        private static List<SelectableBase> _selectables = new List<SelectableBase>();
        
        public static List<SelectableBase> SelectableObjects
        {
            get { return _selectables; }
        }

        public static void AddSelectable(SelectableBase selectable)
        {
            if (!_selectables.Contains(selectable))
            {
                _selectables.Add(selectable);
            }
        }

        public static void RemoveSelectable(SelectableBase selectable)
        {
            _selectables.Remove(selectable);
        }
    }
}