using UnityEngine;

namespace Game.Camera
{
    public class PlayerCameraView : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTarget;

        private void Start()
        {
            VirtualCameraView.Instance.SetupTarget(_cameraTarget);
        }
    }
}