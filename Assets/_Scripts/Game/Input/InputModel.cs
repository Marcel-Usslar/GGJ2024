using UnityEngine;
using Utility;
using Utility.Singletons;

namespace Game.Input
{
    public class InputModel : SingletonModel<InputModel>
    {
        public Vector2 Input { get; private set; }
        public Vector2 NormalizedInput { get; private set; }
        public Vector2 LastNormalizedInput { get; private set; } = Vector2.up;

        public CallbackHandler OnInteract { get; } = new();

        public void UpdateInput(Vector2 input)
        {
            LastNormalizedInput = NormalizedInput == Vector2.zero ? LastNormalizedInput : NormalizedInput;
            Input = input;
            NormalizedInput = input == Vector2.zero ? Vector2.zero : input.normalized;
        }

        public void TriggerInteraction()
        {
            OnInteract.Trigger();
        }

        public void Reset()
        {
            LastNormalizedInput = Vector2.up;
            NormalizedInput = Vector2.zero;
            Input = Vector2.zero;
        }
    }
}