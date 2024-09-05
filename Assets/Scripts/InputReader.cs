using UnityEngine;
using UnityEngine.EventSystems;

public class InputReader : MonoBehaviour, IInputReader
{
    private KeyCode _pushKey = KeyCode.Space;
    private bool _isPush;

    public float Direction { get; private set; }

    private void Update()
    {
        if (TimeManager.IsPaused)
            return;

        Direction = Input.GetAxis(ConstantsData.InputData.HORIZONTAL_AXIS);

        if (Input.GetKeyDown(_pushKey))
            _isPush = true;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;
    }

    public bool GetIsPush() => GetBoolAsTrigger(ref _isPush);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool lockalValue = value;
        value = false;
        return lockalValue;
    }
}