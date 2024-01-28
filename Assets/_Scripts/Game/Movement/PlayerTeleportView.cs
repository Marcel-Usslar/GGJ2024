using Game.Area;
using Game.Camera;
using UnityEngine;

namespace Game.Movement
{
    public class PlayerTeleportView : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private PlayerMovement _playerMovement;

        private void Start()
        {
            AreaManagementService.Instance.CurrentArea.RegisterCallback(Teleport);
        }

        private void OnDestroy()
        {
            AreaManagementService.Instance.CurrentArea.UnregisterCallback(Teleport);
        }

        private void Teleport(AreaId id)
        {
            var position = AreaManagementService.Instance.GetAreaPosition(id);
            _player.position = position;
            _playerMovement.Position.Value = position;
        }
    }
}