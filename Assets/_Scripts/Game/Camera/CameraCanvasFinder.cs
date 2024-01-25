using UnityEngine;

namespace Game.Camera
{
    public class CameraCanvasFinder : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void Start()
        {
            _canvas.worldCamera = CameraView.Instance.Camera;
        }

        private void OnValidate()
        {
            if (ReferenceEquals(_canvas, null) || _canvas.Equals(null))
                _canvas = GetComponent<Canvas>();
        }
    }
}