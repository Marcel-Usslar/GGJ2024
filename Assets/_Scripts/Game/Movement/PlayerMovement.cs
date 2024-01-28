using Game.GameState;
using Game.Input;
using UnityEngine;
using Utility;

namespace Game.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [Space]
        [SerializeField] private LayerMask _hitLayerMask;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _directionIndicator;

        public ReactiveProperty<Vector2> Position { get; } = new();

        private void FixedUpdate()
        {
            if (GameStateModel.Instance.IsPaused.Value)
            {
                Position.Value = _rigidbody.position;
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            var input = InputModel.Instance.Input.magnitude < 1
                ? InputModel.Instance.Input
                : InputModel.Instance.NormalizedInput;
            var deltaTime = Time.fixedDeltaTime;

            var hit = Physics2D.Raycast(_rigidbody.position, input, 1, _hitLayerMask);
            if (hit.collider != null)
            {
                input = CalculateInputAfterCollision(input, hit.normal);
            }

            _rigidbody.velocity = input * deltaTime * _speed;
            Position.Value = _rigidbody.position;

            var direction = InputModel.Instance.NormalizedInput == Vector2.zero
                ? InputModel.Instance.LastNormalizedInput
                : InputModel.Instance.NormalizedInput;
            _directionIndicator.gameObject.SetActive(true);
            _directionIndicator.localRotation = Quaternion.LookRotation(direction);
        }

        public Vector2 CalculateInputAfterCollision(Vector2 input, Vector2 collisionNormal)
        {
            //Calculate new direction from conditions (I) n * t = 0 and (II) ||t|| = ||input||

            //Debug.LogError("input is currently: [0] " + input[0] + ", [1] " + input[1]);
            //Debug.LogError("collisionNormal is currently: [0] " + collisionNormal[0] + ", [1] " + collisionNormal[1]);

            Vector2 output = new Vector2(0, 0);

            //Try clean solution
            float cons = input.sqrMagnitude / collisionNormal.sqrMagnitude;
            float t_1_1 = collisionNormal[1] * cons;
            float t_2_1 = -collisionNormal[0] * cons;

            float t_1_2 = -collisionNormal[1] * cons;
            float t_2_2 = collisionNormal[0] * cons;

            //Check angles wrt to input defined by possible tangent vectors
            if (Mathf.Abs(t_1_1 * input[0] + t_2_1 * input[1]) == 0)
            {
                output = new Vector2(0, 0);
            }
            else if (t_1_1 * input[0] + t_2_1 * input[1] > 0)
            {
                output = new Vector2(t_1_1, t_2_1);
            }
            else
            {
                output = new Vector2(t_1_2, t_2_2);
            }

            //Debug.LogError("Changed input into: [0] " + output[0] + ", [1] " + output[1]);

            return output;
        }
    }
}
