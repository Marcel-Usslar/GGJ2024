using UnityEngine;

namespace Game.Input
{
    public abstract class InputView : MonoBehaviour
    {
        public abstract bool HasInput { get; }
    }
}