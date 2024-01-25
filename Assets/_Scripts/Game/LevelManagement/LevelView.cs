using UnityEngine;

namespace Game.LevelManagement
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private bool _resetToSameLevel;

        public bool ResetToSameLevel => _resetToSameLevel;
    }
}