using CustomButton;
using UnityEngine;

namespace Game.UI
{
    public class RadioButtonComponent : MonoBehaviour
    {
        [SerializeField] private ReactiveButton _button;
        [SerializeField] private GameObject _selectedIcon;

        public IButton Button => _button;

        public bool IsSelected { set => _selectedIcon.SetActive(value); }
    }
}