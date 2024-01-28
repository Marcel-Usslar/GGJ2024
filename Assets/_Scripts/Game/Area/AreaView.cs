using UnityEngine;

namespace Game.Area
{
    public class AreaView : MonoBehaviour
    {
        [SerializeField] private AreaId _areaId;

        public AreaId AreaId => _areaId;
        public Vector2 Position => transform.position;

        private void Start()
        {
            AreaManagementService.Instance.RegisterArea(this);
        }

        private void OnDestroy()
        {
            AreaManagementService.Instance.UnregisterArea(this);
        }
    }
}