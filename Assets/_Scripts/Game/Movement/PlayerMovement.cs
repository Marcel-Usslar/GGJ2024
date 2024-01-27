using Game.GameState;
using Game.Input;
using UnityEngine;

namespace Game.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [Space]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _directionIndicator;

        private void FixedUpdate()
        {
            if (GameStateModel.Instance.IsPaused.Value)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            var input = InputModel.Instance.Input.magnitude < 1
                ? InputModel.Instance.Input
                : InputModel.Instance.NormalizedInput;
            var deltaTime = Time.fixedDeltaTime;

            _rigidbody.velocity = input * deltaTime * _speed;

            var direction = InputModel.Instance.NormalizedInput == Vector2.zero
                ? InputModel.Instance.LastNormalizedInput
                : InputModel.Instance.NormalizedInput;
            _directionIndicator.gameObject.SetActive(true);
            _directionIndicator.localRotation = Quaternion.LookRotation(direction);
        }
    }
}