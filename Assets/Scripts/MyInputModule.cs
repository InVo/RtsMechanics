
using UnityEngine;
using UnityEngine.EventSystems;

public class MyInputModule : BaseInputModule
{
    public override void Process()
    {
        ExecuteEvents.Execute(gameObject, new BaseEventData(eventSystem), ExecuteEvents.moveHandler);
    }
}
