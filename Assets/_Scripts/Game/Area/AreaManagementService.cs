using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Singletons;

namespace Game.Area
{
    public class AreaManagementService : SingletonModel<AreaManagementService>
    {
        private readonly Dictionary<AreaId, AreaView> _areas = new();

        public CallbackHandler<Vector2> OnTeleport { get; } = new();

        public void TeleportTo(AreaId id)
        {
            if (!_areas.ContainsKey(id))
            {
                Debug.LogError($"Can't teleport to area {id}");
                return;
            }

            var area = _areas[id];
            OnTeleport.Trigger(area.Position);
        }

        public void RegisterArea(AreaView view)
        {
            if (_areas.ContainsKey(view.AreaId))
            {
                Debug.LogError($"Area {view.AreaId} was already registered!");
                return;
            }

            _areas[view.AreaId] = view;
        }

        public void UnregisterArea(AreaView view)
        {
            if (!_areas.ContainsKey(view.AreaId))
            {
                Debug.LogError($"Area {view.AreaId} was never registered!");
                return;
            }

            _areas.Remove(view.AreaId);
        }
    }
}