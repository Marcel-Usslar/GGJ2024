using Game.Interaction;
using UnityEngine;

namespace Game.Area
{
    public class AreaSwitchInteractableItemView : InteractableItemView
    {
        [SerializeField] private AreaId _areaId;

        public override void Interact()
        {
            AreaManagementService.Instance.TeleportTo(_areaId);
        }
    }
}