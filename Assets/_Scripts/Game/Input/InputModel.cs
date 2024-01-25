using Game.Utility;
using UnityEngine;
using Utility;
using Utility.Singletons;

namespace Game.Input
{
    public class InputModel : SingletonModel<InputModel>
    {
        private Vector2 _input;
        private Vector2 _normalizedInput;
        private Vector2 _lastNormalizedInput = Vector2.up;

        public Vector2 Input => _input;
        public Vector2 NormalizedInput => _normalizedInput;
        public Vector2 LastNormalizedInput => _lastNormalizedInput;
        public Vector3 WorldSpaceInput => new Vector3(_input.x, 0, _input.y);
        public Vector3 NormalizedWorldSpaceInput => new Vector3(_normalizedInput.x, 0, _normalizedInput.y);
        public Vector3 LastNormalizedWorldSpaceInput => new Vector3(_lastNormalizedInput.x, 0, _lastNormalizedInput.y);

        public void UpdateInput(Vector2 input)
        {
            _lastNormalizedInput = _normalizedInput == Vector2.zero ? _lastNormalizedInput : _normalizedInput;
            _input = input;
            _normalizedInput = input == Vector2.zero ? Vector2.zero : input.normalized;
        }

        public void Reset()
        {
            _lastNormalizedInput = Vector2.up;
            _normalizedInput = Vector2.zero;
            _input = Vector2.zero;
        }
    }
}