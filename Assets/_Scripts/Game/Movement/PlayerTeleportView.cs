using Game.Area;
using UnityEngine;

namespace Game.Movement
{
    public class PlayerTeleportView : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private PlayerMovement _playerMovement;

        private void Start()
        {
            AreaManagementService.Instance.OnTeleport.RegisterCallback(Teleport);
        }

        private void OnDestroy()
        {
            AreaManagementService.Instance.OnTeleport.UnregisterCallback(Teleport);
        }

        private void Teleport(Vector2 position)
        {
            _player.position = position;
            _playerMovement.Position.Value = position;
        }
    }
}