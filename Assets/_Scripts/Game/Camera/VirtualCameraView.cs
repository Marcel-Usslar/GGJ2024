using Cinemachine;
using UnityEngine;
using Utility.Singletons;

namespace Game.Camera
{
    public class VirtualCameraView : SingletonMonoBehaviour<VirtualCameraView>
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera1;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera2;

        private bool _usingCamera1 = true;

        protected override void OnInitialize()
        {
            ToggleVirtualCameras();
        }

        public void SetupTarget(Transform target)
        {
            _virtualCamera1.Follow = target;
            _virtualCamera1.LookAt = target;
            _virtualCamera2.Follow = target;
            _virtualCamera2.LookAt = target;
        }

        public void Toggle()
        {
            _usingCamera1 = !_usingCamera1;
            ToggleVirtualCameras();
        }

        private void ToggleVirtualCameras()
        {
            _virtualCamera1.gameObject.SetActive(_usingCamera1);
            _virtualCamera2.gameObject.SetActive(!_usingCamera1);
        }
    }
}