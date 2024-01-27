using CustomButton;
using UnityEngine;

namespace Game.UI
{
    public class PanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _menuRoot;
        [SerializeField] private ReactiveButton _closeButton;

        public ReactiveButton CloseButton => _closeButton;

        public bool Visible { set => _menuRoot.SetActive(value); }
    }
}