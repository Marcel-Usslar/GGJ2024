using Game.Camera;
using UnityEngine;

namespace Game.Area
{
    public class AreaCameraView : MonoBehaviour
    {
        [SerializeField] private GameObject _roomCamera;
        [SerializeField] private AreaView _areaView;

        private void Start()
        {
            AreaManagementService.Instance.CurrentArea.RegisterCallback(ToggleCamera);
        }

        private void OnDestroy()
        {
            AreaManagementService.Instance.CurrentArea.UnregisterCallback(ToggleCamera);
        }

        private void ToggleCamera(AreaId id)
        {
            var isActive = _areaView.AreaId == id;
            _roomCamera.SetActive(isActive);
        }
    }
}