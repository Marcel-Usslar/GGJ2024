using UnityEngine;
using UnityEngine.EventSystems;

// Taken from here
// http://ilkinulas.github.io/programming/unity/2016/03/18/unity_ui_drag_threshold.html

namespace Utility
{
    public class DragThresholdFromDPI : MonoBehaviour
    {
        private const float DPIRatio = 160f;

        private void Start()
        {
            var defaultValue = EventSystem.current.pixelDragThreshold;
            EventSystem.current.pixelDragThreshold =
                Mathf.Max(
                    defaultValue,
                    (int) (defaultValue * Screen.dpi / DPIRatio));
        }
    }
}