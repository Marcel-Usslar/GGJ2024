using DG.Tweening;
using NaughtyAttributes;
using Plugins.TweenAnimationSystem;
using UnityEngine;
using UnityEngine.UI;
using CustomButton;

namespace Game.UI
{
    public class ToggleComponent : MonoBehaviour
    {
        [SerializeField] private ReactiveButton _button;
        [SerializeField] private Image _toggleImage;
        [Space]
        [SerializeField] private Color _onColor;
        [SerializeField] private Color _offColor;
        [Space]
        [SerializeField] private float _slideDuration;
        [Space]
        [SerializeField] private RectTransform _toggleContainer;
        [SerializeField] private RectTransform _toggleButton;

        public IButton Button => _button;

        private Tween _onTween;
        private Tween _offTween;


        public bool IsOn
        {
            set => PlayToggleTween(value);
        }

        private void PlayToggleTween(bool value)
        {
            GetTween(value).Restart();
        }

        private Tween GetTween(bool value)
        {
            Tween tween;
            if (value)
                tween = _onTween ??= CreateToggleTween(true);
            else
                tween = _offTween ??= CreateToggleTween(false);

            return tween;
        }

        private Tween CreateToggleTween(bool value)
        {
            var position = GetPosition(value);
            var color = value ? _onColor : _offColor;

            _toggleButton.anchoredPosition = new Vector2(GetPosition(!value), _toggleButton.anchoredPosition.y);

            return DOTween.Sequence()
                .Append(_toggleButton.DOAnchorPosX(position, _slideDuration))
                .AppendCallback(() => _toggleImage.color = color)
                .SetAutoKill(false)
                .Pause()
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private float GetPosition(bool value)
        {
            return value
                ? _toggleContainer.rect.width - _toggleButton.rect.width
                : 0;
        }


        private bool _toggleState = false;

        [Button(nameof(Toggle))]
        private void Toggle()
        {
            _toggleState = !_toggleState;
            var tween = CreateToggleTween(_toggleState);
            tween.PlayOnceInEditor();
        }
    }
}